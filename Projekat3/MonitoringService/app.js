const express = require('express');
const mqtt = require('mqtt');


const methodsMiddleware = require('./middlewares/methods');
const {limitsRoutes, limitInstance} = require('./routes/limits');
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
    console.log('Temp limit: ', limitInstance.getTemperatureLimit(), 'Hum limit: ', limitInstance.getHumidityLimit(),'Pres limit: ', limitInstance.getPressureLimit())
    let newColor = null;
    switch(parameterName){
        case 'temperature':
            if (patameterValue>limitInstance.getTemperatureLimit()){
                limitInstance.incrTemperatureAboveLimit()
                console.log(patameterValue, limitInstance.getTemperatureAboveLimit())
                if(limitInstance.getTemperatureAboveLimit()===5)
                    newColor='red'
                else if(limitInstance.getTemperatureAboveLimit()===3)
                    newColor='orange'                  
            } else if(limitInstance.getTemperatureAboveLimit()>0){
                limitInstance.clearTemperatureAboveLimit();
                newColor='green'
            }
            break;
        case 'humidity':
            if (patameterValue>limitInstance.getHumidityLimit()){
                limitInstance.incrHumidityAboveLimit()
                console.log(patameterValue, limitInstance.getHumidityAboveLimit())
                if(limitInstance.getHumidityAboveLimit()===5)
                    newColor='red'
                else if(limitInstance.getHumidityAboveLimit()===3)
                    newColor='orange'
                
            } else if(limitInstance.getHumidityAboveLimit()>0){
                limitInstance.clearHumidityAboveLimit();
                newColor='green'
            }
            break;
        case 'pressure':
            if (patameterValue>limitInstance.getPressureLimit()){
                limitInstance.incrPressureAboveLimit()
                console.log(patameterValue, limitInstance.getPressureAboveLimit())
                if(limitInstance.getPressureAboveLimit()===5)
                    newColor='red'
                else if(limitInstance.getPressureAboveLimit()===3)
                    newColor='orange'            
            } else if(limitInstance.getPressureAboveLimit()>0){
                limitInstance.clearPressureAboveLimit();
                newColor='green'
            }     
            break;     
    }
    if(newColor)
        sendCommand(newColor, parameterName);
    
})


module.exports = app;