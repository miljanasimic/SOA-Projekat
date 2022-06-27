const fs = require('fs');
const { parse } = require('csv-parse');
const axios = require('axios').default;

function readData(firstLine, lastLine) {
  let records = [];
    fs.createReadStream('./lapTimes.csv', { encoding : 'utf-8' })
        .pipe(parse({ delimiter : ',', from_line: 1, columns: true }))
        .on('data', (chunk) => {
            records.push(chunk);
            if(records.length > 50){
              sendRequests(records.slice())
            }
        });
}

async function sendRequests(records){
  records.forEach(async (chunk)=>{
    setInterval(async () => {
      axios.post('http://localhost:5000/LapTimes', chunk)
            .then(function (response) {
              })
              .catch(function (error) {
                console.log(error.code);
              });
    }, 5000);
  });
}

const maxLine=400000;
const offset=3;
let firstLine=1;
let lastLine=3;
setInterval(() => {
    readData(firstLine,lastLine);
    firstLine+=offset
    lastLine=lastLine+offset< maxLine ? lastLine+offset : maxLine
}, 300);
