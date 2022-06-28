using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnalyticsService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();

            using(var httpClient = httpClientFactory.CreateClient("EkuiperHttpClient"))
            {
                await EkuiperWrapper.ConfigureStream(httpClient);
                await EkuiperWrapper.ConfigureQuery(httpClient);
            }

            await MqttHelper.SubscribeToTopic("mqtt", 1883, "lap-data", MqttHelper.onGatewayMessageReceived);
            await MqttHelper.SubscribeToTopic("mqtt", 1883, "ekuiper-output", MqttHelper.onEkuiperMessageReceived);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
