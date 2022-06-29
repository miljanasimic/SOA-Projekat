using Grpc.Net.Client;
using System;

namespace GrpcClient
{
    public static class GrpcMethods
    {
        public async static void NotifyService(MessageRequest message)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress("http://notificationservice:50051");
                var client = new NotificationService.NotificationServiceClient(channel);
                var reply = await client.NotifyAsync(message);
                Console.WriteLine("Notification service notified...");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
