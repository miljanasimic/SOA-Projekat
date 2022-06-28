using AnalyticsService.DataClasses;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsService
{
    public class MqttHelper
    {
        public static async Task PublishToTopic(string server, int port, string topic, string payload)
        {
            var mqttFactory = new MqttFactory();
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(server, port)
                    .Build();

                mqttClient.ConnectedAsync += async (e) =>
                {
                    //Console.WriteLine("Connected.");
                };
                mqttClient.DisconnectedAsync += async (e) =>
                {
                    //Console.WriteLine("Disconnected.");
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            }
        }

        public static async Task SubscribeToTopic(string server, int port, string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> handler)
        {
            var mqttFactory = new MqttFactory();
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(server, port)
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += handler;

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public static async Task onGatewayMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Received message from the gateway api...");
            var payloadAsString = Encoding.Default.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine(payloadAsString);

            await PublishToTopic("mqtt", 1883, "ekuiper-input", payloadAsString);
        }

        public static async Task onEkuiperMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Received results from ekuiper...");
            var payloadAsString = Encoding.Default.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine(payloadAsString);

            var messages = JsonConvert.DeserializeObject<List<LapMessage>>(payloadAsString);

            foreach(var message in messages)
            {
                InfluxDBWritter.WriteToInflux(message);
            }
        }
    }
}
