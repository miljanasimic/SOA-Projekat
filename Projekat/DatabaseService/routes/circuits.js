const express = require("express");
const router = express.Router();
const {Circuit} = require("../models")

router.get("", (req, res) => {
    Circuit.find({})
    .then(response =>{
        const dtoResponse = []
        response.forEach(el=> dtoResponse.push(Circuit.toDTO(el)))
        return res.send(dtoResponse)
    })
    .catch(err=> res.status(409).send(err.message))
})

router.get("/:id", (req, res) => {
    Circuit.findOne({circuitId: parseInt(req.params.id)})
    .then(response => res.send(Circuit.toDTO(response)))
    .catch(err=> res.status(409).send(err.message))
})

module.exports = router;