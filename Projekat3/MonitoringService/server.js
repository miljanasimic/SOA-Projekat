const mqtt = require('mqtt')
const host = 'edgex-mqtt'
const port = 1883
const connectUrl = `mqtt://${host}:${port}`
const client = mqtt.connect(connectUrl)

const topic = 'edgex-data'
client.on("connect", ()=> {
    console.log('Connected')
    client.subscribe(topic, ()=> {
        console.log(`Subscribe to topic '${topic}'`)
    })
})

client.on('message', (topic, payload) => {
    console.log('Received Message:', topic, payload.toString())
  })
  