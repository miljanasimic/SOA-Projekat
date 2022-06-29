var PROTO_PATH = __dirname + '/notification.proto';
var grpc = require('@grpc/grpc-js');
var protoLoader = require('@grpc/proto-loader');

var packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {keepCase: true,
     longs: String,
     enums: String,
     defaults: true,
     oneofs: true
    });

var notificationservice = grpc.loadPackageDefinition(packageDefinition).notificationservice;

function notify(call, callback) {
    console.log(`New best time! Time of ${call.request.milliseconds / 1000} seconds was set by driver with ID ${call.request.driverId}`)
    callback(null, null)
}

/**
 * Get a new server with the handler functions in this file bound to the methods
 * it serves.
 * @return {Server} The new server object
 */
function getServer() {
    var server = new grpc.Server();
    server.addService(notificationservice.NotificationService.service, {
        notify : notify
    });
    return server;
}

function main() {
    var routeServer = getServer();
    routeServer.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
        routeServer.start();
    })
}

main();