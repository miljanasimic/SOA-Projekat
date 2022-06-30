const express = require("express");
const router = express.Router();

const Limit = require("../Limit")
const limitInstance = new Limit()

router.post("/temperature", (req,res)=>{
    if (req.body.newLimit){
        limitInstance.setTemperatureLimit(req.body.newLimit);
        return res.send(`New temperature limit: ${req.body.newLimit}`)
    }
    return res.status(400).send("Temperature limit isn't updated. 'newLimit' parameter is missing");
})

router.post("/pressure", (req,res)=>{
    if (req.body.newLimit){
        limitInstance.setPressureLimit(req.body.newLimit);
        return res.send(`New pressure limit: ${req.body.newLimit}`)
    }
    return res.status(400).send("Pressure limit isn't updated. 'newLimit' parameter is missing");
})

router.post("/humidity", (req,res)=>{
    if (req.body.newLimit){
        limitInstance.setHumidityLimit(req.body.newLimit);
        return res.send(`New humidity limit: ${req.body.newLimit}`)
    }
    return res.status(400).send("Humidity limit isn't updated. 'newLimit' parameter is missing");
})

module.exports = {limitsRoutes: router, limitInstance: limitInstance}