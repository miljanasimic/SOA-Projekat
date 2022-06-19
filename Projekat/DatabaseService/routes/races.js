
const express = require("express");
const router = express.Router();
const {Race} = require("../models")

router.get("", async(req, res)=>{
    Race.find({}, 'raceId year')
    .then(response => {
        const dtoResponse = []
        response.forEach(el=> dtoResponse.push(Race.toDTO(el)))
        return res.send(dtoResponse)
    })
    .catch(err=> res.status(400).send(err.message))
})

router.get("/:id", async(req, res)=>{
    Race.findOne({raceId: parseInt(req.params.id)}, 'raceId year')
    .then(response => {
        if(response)
            return res.send(Race.toDTO(response))
        return res.status(404).send(`GET Request faild! Race with id ${req.params.id} not found`)
    })
    .catch(err=> res.status(400).send(err.message))
})

router.post("", async(req, res)=>{
    const race = new Race({
        raceId: req.body.raceId,
        year: req.body.year,
        round: req.body.round,
        circuitId: req.body.circuitId,
        name: req.body.name,
        date: req.body.date,
        time: req.body.time,
        url: req.body.url
    })

    race.save()
    .then(result=>{
        console.log(result)
        res.send("Race created successfully")
    })
    .catch(err => res.status(400).send(err.message))
    
})

router.put("/:id", async(req, res)=> {
    if (!req.params.id)
        return res.status(400).send(`PUT Request faild! RaceId is missing`)
    const race = await Race.findOne({raceId : parseInt(req.params.id)})
    if (!race) 
        return res.status(404).send(`PUT Request faild! Race with id ${req.params.id} not found`)

    if (req.body.year)
        race.year = req.body.year
    if (req.body.round)
        race.round = req.body.round
    if (req.body.circuitId)
        race.circuitId = req.body.circuitId
    if (req.body.name)
        race.name = req.body.name
    if (req.body.date)
        race.date = req.body.date
    if (req.body.time)
        race.time = req.body.time
    if (req.body.url)
        race.url= req.body.url

    race.save()
    .then((result)=>{
        console.log(result)
        res.send("Race updated successfully")
    })
    .catch(err => res.status(500).send(err))    
})

router.delete("/:id", async(req, res)=>{
    Race.deleteOne({raceId: parseInt(req.params.id)})
    .then((result)=>{
        console.log(result)
        if (result.deletedCount===0)
            return res.status(404).send(`DELETE Request faild! Race with id ${req.params.id} not found`)
        return res.send("Race deleted successfully")
    })
    .catch(err => res.status(500).send(err))    
})

module.exports = router;