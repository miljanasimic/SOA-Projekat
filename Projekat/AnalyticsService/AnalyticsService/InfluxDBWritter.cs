using AnalyticsService.DataClasses;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using System;

namespace AnalyticsService
{
    public static class InfluxDBWritter
    {
        private static readonly string TOKEN = "token";
        private static readonly string BUCKET = "laptimes";
        private static readonly string ORG = "soa_um";
        private static readonly string URL = "http://influx:8086";
        public static void WriteToInflux(LapMessage message)
        {
            try
            {
                var influxDBClient = InfluxDBClientFactory.Create(URL, TOKEN);

                using (var writeApi = influxDBClient.GetWriteApi())
                {
                    var measurement = new LapTimeMeasurement
                    {
                        DriverId = message.DriverId,
                        Milliseconds = message.Milliseconds
                    };

                    writeApi.WriteMeasurement(measurement, WritePrecision.Ns, BUCKET, ORG);
                }

                influxDBClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
