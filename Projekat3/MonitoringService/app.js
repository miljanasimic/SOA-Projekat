const express = require('express');
const mqtt = require('mqtt');


const methodsMiddleware = require('./middlewares/methods');
const {limitsRoutes, getHumidityLimit, getPressureLimit, getTemperatureLimit,
    getTemperatureAboveLimit, getHumidityAboveLimit, getPressureAboveLimit,
    incrTemperatureAboveLimit, incrHumidityAboveLimit, incrPressureAboveLimit} = require('./routes/limits');
const convertToFloat = require('./converter');
const sendCommand = require('./command');

const app = express();

app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use((req, res, next) => methodsMiddleware(req, res, next));
app.use("/limits", limitsRoutes);

const host = 'edgex-mqtt'
const portTopic = 1883
const connectUrl = `mqtt://${host}:${portTopic}`
const client = mqtt.connect(connectUrl)
const topic = 'edgex-data'

client.on("connect", ()=> {
    console.log('Connected')
    client.subscribe(topic, ()=> {
        console.log(`Subscribed to topic '${topic}'`)
    })
})

client.on('message', (topic, payload) => {
    const dataReceived=JSON.parse(payload.toString())
    const readings = dataReceived.readings
    const parameterName = readings[0].name
    const patameterValue = convertToFloat(readings[0].value)
  
    console.log('Received Message:',parameterName,patameterValue)
    console.log('Temp limit: ', getTemperatureLimit())
    console.log('Hum limit: ', getHumidityLimit())
    console.log('Pres limit: ', getPressureLimit())
    switch(parameterName){
        case 'temperature':
            if (patameterValue>getTemperatureLimit()){
                incrTemperatureAboveLimit()
                
            }
        case 'humidity':
        if (patameterValue>getHumidityLimit()){
            incrHumidityAboveLimit()
            
        }
        case 'pressure':
        if (patameterValue>getPressureLimit()){
            incrPressureAboveLimit()            
        }            
    }
    sendCommand("red");
    
})


module.exports = app;