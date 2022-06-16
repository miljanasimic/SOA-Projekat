"use strict"

const langDetect = require("languagedetect")
const langDetector = new langDetect()

module.exports = async (context, callback) => {
    const probLanguage = langDetector.detect(context)[0];
    const percentage = Math.round(probLanguage[1] * 100);
    return `I am ${percentage} % sure this is ${probLanguage[0]} language.`
}
