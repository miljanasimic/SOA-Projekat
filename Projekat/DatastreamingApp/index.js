const fs = require('fs');
const { parse } = require('csv-parse');
const axios = require('axios').default;

function readData() {
    const data=[]
    fs.createReadStream('./lapTimes.csv', { encoding : 'utf-8' })
        .pipe(parse({ delimiter : ',', from_line: 1, columns: true }))
        .on('data', chunk=>{
            data.push(chunk)
        })
    
    console.log(data)
    return data
}

