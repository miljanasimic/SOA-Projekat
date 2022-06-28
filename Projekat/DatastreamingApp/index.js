const fs = require('fs');
const { parse } = require('csv-parse');
const axios = require('axios').default;

function readData(firstLine, lastLine) {
    fs.createReadStream('./lapTimes.csv', { encoding : 'utf-8' })
        .pipe(parse({ delimiter : ',', from_line: firstLine, to_line: lastLine, 
        columns: ["raceId","driverId","lap","position","time","milliseconds"]}))
        .on('data', (chunk) => {
            axios.post('http://localhost:5000/LapTimes', chunk)
              .catch(function (error) {
                console.log(error.code);
              });
        });
}

function main(offset, maxline, freq){
  let firstLine=2;
  let lastLine=firstLine+offset;
  setInterval(() => {
      readData(firstLine,lastLine);    
      firstLine+=offset+1
      lastLine=lastLine+offset+1< maxline ? lastLine+offset+1 : maxline      
  }, freq);
}


const maxLine=400000;
const offset=5;
const freq = 200;
main(offset, maxLine, freq);

