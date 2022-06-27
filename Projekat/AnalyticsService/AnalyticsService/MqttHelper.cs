using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsService
{
    public class MqttHelper
    {
        public static async Task PublishToTopic(string server, int port, string topic)
        {
            var mqttFactory = new MqttFactory();
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(server, port)
                    .Build();

                mqttClient.ConnectedAsync += async (e) =>
                {
                    Console.WriteLine("Connected.");
                };
                mqttClient.DisconnectedAsync += async (e) =>
                {
                    Console.WriteLine("Disconnected.");
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload("19.5")
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            }
        }

        public static async Task SubscribeToTopic(string server, int port, string topic)
        {
            var mqttFactory = new MqttFactory();
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(server, port)
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += async (e) =>
            {
                await onMessageReceived(e);
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        private static async Task onMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Received message");
        }
    }
}
