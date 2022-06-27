const fs = require('fs');
const { parse } = require('csv-parse');
const axios = require('axios').default;

function readData() {
    fs.createReadStream('./lapTimes.csv', { encoding : 'utf-8' })
        .pipe(parse({ delimiter : ',', from_line: 1, columns: true }))
        .on('data', chunk=> {
            setInterval(() => {
                axios.post('http://gatewayapi:5000/laps', chunk)
                .then(function (response) {
                    console.log(response);
                  })
                .catch(function (error) {
                console.log(error);
                });
            }, 1000);
        });
}

readData();


