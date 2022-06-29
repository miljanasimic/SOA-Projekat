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

function main() {
    var client = new notificationservice.NotificationService('localhost:50051', grpc.credentials.createInsecure());
    client.notify({driverId: 1, milliseconds : 1500}, function(err, reply) {
        if (err) {
          console.log(err)
        }
      });
}

main();