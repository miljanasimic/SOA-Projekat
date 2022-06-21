const mongoose = require("mongoose");
const {CircuitDTO } = require("./DTOs/CircuitDTO")
const {RaceDTO } = require("./DTOs/RaceDTO")
const {DriverDTO } = require("./DTOs/DriverDTO")
var uniqueValidator = require('mongoose-unique-validator');

const driverSchema = new mongoose.Schema({
    driverId: {
        type: Number,
        required: true,
        index: { unique: true }
    },
    driverRef: String,
    number: Number,
    code: String,
    forename: String,
    surname: String,
    dob: String,
    nationality: String,
    url: String
});
driverSchema.statics.toDTO = function (modelObject) {
    return new DriverDTO(modelObject.driverId,
        modelObject.forename,
        modelObject.surname,
        modelObject.url,
        modelObject.dob)
}
driverSchema.plugin(uniqueValidator);

const circuitSchema = new mongoose.Schema({
    circuitId: {
        type: Number,
        required: true,
        index: { unique: true }
    },
    circuitRef: String,
    name: String,
    location: String,
    country: String,
    lat: Number,
    lng: Number,
    alt: Number,
    url: String
});

circuitSchema.statics.toDTO = function(modelObject) {
    return new CircuitDTO(modelObject.name,
        modelObject.circuitRef,
        modelObject.lat,
        modelObject.lng,
        modelObject.alt,
        modelObject.url)
}
circuitSchema.plugin(uniqueValidator);

const raceSchema = new mongoose.Schema({
    raceId: {
        type: Number,
        required: true,
        index: { unique: true }
    },
    year: Number,
    round: Number,
    circuitId: Number,
    name: String,
    date: String,
    time: String,
    url: String
});
raceSchema.statics.toDTO = function(modelObject) {
    return new RaceDTO(modelObject.raceId, modelObject.url, modelObject.date, modelObject.time);
}
raceSchema.plugin(uniqueValidator);

const Driver = mongoose.model('Driver', driverSchema);
const Circuit = mongoose.model('Circuit', circuitSchema);
const Race = mongoose.model('Race', raceSchema);

module.exports = { Driver : Driver, Circuit : Circuit, Race : Race };