using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisualizationService.DataClasses;

namespace VisualizationService
{
    public static class MqttHelper
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
                await onEdgexDataReceived(e);
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public static async Task onEdgexDataReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                Console.WriteLine("Received results from EdgeX...");
                var payloadAsString = Encoding.Default.GetString(e.ApplicationMessage.Payload);
                var edgexMessage = JsonConvert.DeserializeObject<DeviceReadingModel>(payloadAsString);

                foreach (var reading in edgexMessage.Readings)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(reading));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
