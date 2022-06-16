const express = require('express');
const path = require('path');
const methodsMiddleware = require('./middlewares/methods');

const app = express();

app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use((req, res, next) => methodsMiddleware(req, res, next));

module.exports = app;