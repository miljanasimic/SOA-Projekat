const express = require("express");
const router = express.Router();
const {Driver} = require("../models")

router.get("", async(req, res)=>{
    Driver.find({
        $or : [{ forename : {$regex : req.query.search}}, 
               { surname : {$regex : req.query.search}},
               { driverRef : {$regex : req.query.search}}]
    }, 'driverId forename surname url dob')
    .then(response => {
        const dtoResponse = []
        response.forEach(el=> dtoResponse.push(Driver.toDTO(el)))
        return res.send(dtoResponse)
    })
    .catch(err=> res.status(400).send(err.message))    
})

router.get("/:code", async(req, res)=>{
    Driver.findOne({code: req.params.code.toUpperCase()}, 'driverId forename surname url dob')
    .then(response => {
        if(response)
            return res.send(Driver.toDTO(response))
        return res.status(404).send(`GET Request failed! Driver with code ${req.params.code} not found`)
    })
    .catch(err=> res.status(400).send(err.message))    
})

router.post("", async(req, res)=>{
    const driver = new Driver({
    driverId: req.body.driverId,
    driverRef: req.body.driverRef,
    number: req.body.number,
    code: req.body.code,
    forename: req.body.forename,
    surname: req.body.surname,
    dob: req.body.birthdate,
    nationality: req.body.nationality,
    url: req.body.url
    })

    driver.save()
    .then(()=> res.send("Driver created successfully"))
    .catch(err=> res.status(400).send(err.message))
})

router.put("/:id", async(req, res)=> {
    if (!req.params.id)
        return res.status(400).send(`PUT Request failed! DriverId is missing`)
    const driver = await Driver.findOne({driverId : parseInt(req.params.id)})
    if (!driver) 
        return res.status(404).send(`PUT Request failed! Driver with id ${req.params.id} not found`)

    if (req.body.driverRef)
        driver.driverRef = req.body.driverRef
    if (req.body.number)
        driver.number = req.body.number
    if (req.body.code)
        driver.code = req.body.code
    if (req.body.forename)
        driver.forename = req.body.forename
    if (req.body.dob)
        driver.dob = req.body.dob
    if (req.body.nationality)
        driver.nationality = req.body.nationality
    if (req.body.url)
        driver.url= req.body.url

    driver.save()
    .then(()=> res.send(Driver.toDTO(driver)))
    .catch(err=> res.status(500).send(err.message))    

})

router.delete("/:id", async(req, res)=>{
    Driver.deleteOne({driverId: parseInt(req.params.id)})
    .then(result=>{
        if (result.deletedCount===0)
            return res.status(404).send(`DELETE Request failed! Driver with id ${req.params.id} not found`)
        return res.send("Driver deleted successfully")
    })
    .catch(err => res.status(500).send(err.message)) 
})

module.exports = router;