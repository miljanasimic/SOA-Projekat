using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AnalyticsService
{
    public static class EkuiperWrapper
    {
        public static int MILLISECONDS = 115000;
        public async static Task ConfigureStream(HttpClient httpClient)
        {
            try
            {
                var streamCommand = "create stream mqtt_input(driverId bigint, time string, milliseconds bigint) " +
                    "WITH(datasource = \"ekuiper-input\", FORMAT = \"json\")";

               var content = JsonContent.Create<object>(new { sql = streamCommand });
                using (var response = await httpClient.PostAsync("/streams", content))
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async static Task ConfigureQuery(HttpClient httpClient)
        {
            try
            {
                var actions = new List<object>();
                actions.Add(new { mqtt = new { server = "tcp://mqtt:1883", topic = "ekuiper-output" } });

                var rule = "select * from mqtt_input where milliseconds < " + MILLISECONDS;
                var content = JsonContent.Create<object>(new { id = "rule", sql = rule, actions = actions });
                using (var response = await httpClient.PostAsync("/rules", content))
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
