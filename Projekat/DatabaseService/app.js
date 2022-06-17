const express = require('express');
const path = require('path');
const methodsMiddleware = require('./middlewares/methods');
const mongoose = require('mongoose');
const { insertData } = require('./database_helpers/mongodb');

const { Driver, Circuit, Race } = require('./models');
const driversRoutes = require("./routes/drivers")
const racesRoutes = require("./routes/races")

const app = express();

app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use((req, res, next) => methodsMiddleware(req, res, next));
app.use("/drivers", driversRoutes)
app.use("/races", racesRoutes)

const uri = "mongodb://localhost:27017/f1-data?retryWrites=true&w=majority";

mongoose.connect(uri).then(() => {
    console.log('Connection to database successfull.')
    fillDatabase().then(() => {
        console.log('Database filled successfully.');
    }).catch(err => {
        console.log(err);
        console.log('Error occured while populating the database...');
    });
}).catch(err => {
    console.log(err);
    console.log('An error occured while connecting to the database.');
});

async function fillDatabase(){
    const driverCount = await mongoose.connection.db.collection('drivers').countDocuments();
    if(driverCount == 0){
        console.log('Driver collection is empty... Inserting drivers...');
        insertData('./f1_data/drivers.csv', Driver);
    }
    const circuitsCount = await mongoose.connection.db.collection('circuits').countDocuments();
    if(circuitsCount == 0){
        console.log('Circuits collection is empty... Inserting circuits...');
        insertData('./f1_data/circuits.csv', Circuit);
    }
    const racesCount = await mongoose.connection.db.collection('races').countDocuments();
    if(racesCount == 0){
        console.log('Races collection is empty... Inserting races...');
        insertData('./f1_data/races.csv', Race);
    }
}

module.exports = app;