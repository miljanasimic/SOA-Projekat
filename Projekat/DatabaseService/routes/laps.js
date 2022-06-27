const express = require("express");
const router = express.Router();
const {Lap} = require("../models")

router.post("", (req, res) => {
    const lap = new Lap({
        raceId: req.body.raceId,
        driverId: req.body.driverId,
        lap: req.body.lap,
        position: req.body.position,
        time: req.body.time,
        milliseconds: req.body.milliseconds
    })
    lap.save()
    .then(()=> res.send("Lap created successfully"))
    .catch(err=> res.status(400).send(err.message))
})

module.exports = router;