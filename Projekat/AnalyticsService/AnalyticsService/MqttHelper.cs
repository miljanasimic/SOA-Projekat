using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsService
{
    public class MqttHelper
    {
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
            var message = JsonConvert.DeserializeObject<LapMessage>(Encoding.Default.GetString(e.ApplicationMessage.Payload));
        }
    }
}
