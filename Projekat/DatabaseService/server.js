const http = require('http');
const app = require('./app');

const port = process.env.PORT || 3000;
app.set('port', port);
const server = http.createServer(app);

server.listen(port, () => {
    console.log(`Server started on port ${port}`);

    //ovde ide upis podataka u mongo inicijalno iz .csv fajla
});