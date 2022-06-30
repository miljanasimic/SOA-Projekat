using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Newtonsoft.Json;
using System;
using VisualizationService.DataClasses;
using VisualizationService.Measurements;

namespace VisualizationService
{
    public static class InfluxDBWritter
    {
        private static readonly string TOKEN = "token";
        private static readonly string BUCKET = "sensordata";
        private static readonly string ORG = "soa_um";
        private static readonly string URL = "http://influx:8086";
        public static void WriteToInflux(Reading reading)
        {
            try
            {
                var influxDBClient = InfluxDBClientFactory.Create(URL, TOKEN);

                using (var writeApi = influxDBClient.GetWriteApi())
                {
                    WriteSensorMeasurement(writeApi, reading);
                }

                influxDBClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void WriteSensorMeasurement(WriteApi writeApi, Reading reading)
        {
            if(reading.Name == "pressure")
            {
                var measurement = new PressureMeasurement
                {
                    Origin = reading.Origin,
                    Value = ConvertToDouble(reading.Value),
                    Time = DateTime.UtcNow
                };

                writeApi.WriteMeasurement(measurement, WritePrecision.Ns, BUCKET, ORG);
            }
            else if(reading.Name == "temperature")
            {
                var measurement = new TemperatureMeasurement
                {
                    Origin = reading.Origin,
                    Value = ConvertToDouble(reading.Value),
                    Time = DateTime.UtcNow
                };

                writeApi.WriteMeasurement(measurement, WritePrecision.Ns, BUCKET, ORG);
            }
            else if(reading.Name == "humidity")
            {
                var measurement = new HumidityMeasurement
                {
                    Origin = reading.Origin,
                    Value = ConvertToDouble(reading.Value),
                    Time = DateTime.UtcNow
                };

                writeApi.WriteMeasurement(measurement, WritePrecision.Ns, BUCKET, ORG);
            }
        }

        private static double ConvertToDouble(string base64Value)
        {
            var bytes = Convert.FromBase64String(base64Value.ToString());
            Array.Reverse(bytes, 0, 8);
            return BitConverter.ToDouble(bytes);
        }
    }
}
