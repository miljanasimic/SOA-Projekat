const express = require("express");
const router = express.Router();

let temperatureLimit = 0.0;
let pressureLimit = 95000.0;
let humidityLimit = 85.0;
let tAboveLimit=0;
let pAboveLimit=0;
let hAboveLimit=0;

router.post("/temperature", (req,res)=>{
    if (req.body.newLimit){
        temperatureLimit = req.body.newLimit;
        return res.send(`New temperature limit: ${temperatureLimit}`)
    }
    return res.status(400).send("Temperature limit isn't updated. 'newLimit' parameter is missing");
})

router.post("/pressure", (req,res)=>{
    if (req.body.newLimit){
        pressureLimit = req.body.newLimit;
        return res.send(`New pressure limit: ${pressureLimit}`)
    }
    return res.status(400).send("Pressure limit isn't updated. 'newLimit' parameter is missing");
})

router.post("/humidity", (req,res)=>{
    if (req.body.newLimit){
        humidityLimit= req.body.newLimit;
        return res.send(`New humidity limit: ${humidityLimit}`)
    }
    return res.status(400).send("Humidity limit isn't updated. 'newLimit' parameter is missing");
})

function getTemperatureLimit(){
    return temperatureLimit;
}
function getHumidityLimit(){
    return humidityLimit;
}
function getPressureLimit(){
    return pressureLimit;
}

function getTemperatureAboveLimit(){
    return tAboveLimit;
}
function getHumidityAboveLimit(){
    return hAboveLimit;
}
function getPressureAboveLimit(){
    return pAboveLimit;
}

function incrTemperatureAboveLimit(){
    return tAboveLimit++;
}
function incrHumidityAboveLimit(){
    return hAboveLimit++;
}
function incrPressureAboveLimit(){
    return pAboveLimit++;
}

module.exports = {getTemperatureLimit, getHumidityLimit, getPressureLimit,
                  getTemperatureAboveLimit, getHumidityAboveLimit, getPressureAboveLimit,
                  incrTemperatureAboveLimit, incrHumidityAboveLimit, incrPressureAboveLimit,
                  limitsRoutes: router}