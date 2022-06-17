const express = require("express");
const router = express.Router();
const {Driver} = require("../models")

router.get("", async(req, res)=>{
    
})

router.post("", async(req, res)=>{
    const driver = new Driver({
    driverId: req.body.driverId,
    driverRef: req.body.driverRef,
    number: req.body.number,
    code: req.body.code,
    forename: req.body.forename,
    surname: req.body.surname,
    dob: req.body.dob,
    nationality: req.body.nationality,
    url: req.body.url
    })

    driver.save()
    .then((result)=>{
        console.log(result)
        res.send("Driver created successfully")
    })
    .catch((err)=>
        res.status(409).send(err.message)
    )
})

router.put("/:id", async(req, res)=> {
    if (!req.params.id)
        return res.status(409).send(`PUT Request faild! DriverId is missing`)
    const driver = await Driver.findOne({driverId : parseInt(req.params.id)})
    if (!driver) 
        return res.status(409).send(`PUT Request faild! Driver with id ${req.params.id} not found`)

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
    .then((result)=>{
        console.log(result)
        res.send("Driver updated successfully")
    })
    .catch((err)=>
        res.status(409).send(err)
    )    

})

router.delete("/:id", async(req, res)=>{
    Driver.deleteOne({driverId: parseInt(req.params.id)})
    .then((result)=>{
        console.log(result)
        if (result.deletedCount===0)
            return res.status(409).send(`DELETE Request faild! Driver with id ${req.params.id} not found`)
        return res.send("Driver deleted successfully")
    })
    .catch((err)=>
        res.status(409).send(err)
    ) 
})

module.exports = router;