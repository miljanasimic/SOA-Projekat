const mongoose = require("mongoose");

const driverSchema = new mongoose.Schema({
    driverId: Number,
    driverRef: String,
    number: Number,
    code: String,
    forename: String,
    surname: String,
    dob: String,
    nationality: String,
    url: String
});

const circuitSchema = new mongoose.Schema({
    circuitId: Number,
    circuitRef: String,
    name: String,
    location: String,
    country: String,
    lat: Number,
    lng: Number,
    alt: Number,
    url: String
});

const raceSchema = new mongoose.Schema({
    raceId: Number,
    year: Number,
    round: Number,
    circuitId: Number,
    name: String,
    date: String,
    time: String,
    url: String
});

const Driver = mongoose.model('Driver', driverSchema);
const Circuit = mongoose.model('Circuit', circuitSchema);
const Race = mongoose.model('Race', raceSchema);

module.exports = { Driver : Driver, Circuit : Circuit, Race : Race };