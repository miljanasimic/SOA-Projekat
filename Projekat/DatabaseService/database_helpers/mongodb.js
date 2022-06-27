const fs = require('fs');
const { parse } = require('csv-parse');

function insertData(filename, model){
    let records = [];
    fs.createReadStream(filename, { encoding : 'utf-8' })   //TODO: encoding mozda treba da bude ascii, vidi ako pravi problem
        .pipe(parse({ delimiter : ',', from_line: 1, columns: true }))
        .on('data', chunk => {
            records.push(chunk);
            if(records.length > 50){
                populate(records, model);
                records = [];
            }
        })
        .on('end', () => {
            if(records.length > 0){
                populate(records, model);
            }
        });
}

function populate(data, model){
    model.insertMany(data).then(() => {}).catch((err) => {
        console.log(err);
        console.log('Insert many caused an error...');
    });
}

module.exports = { insertData: insertData }