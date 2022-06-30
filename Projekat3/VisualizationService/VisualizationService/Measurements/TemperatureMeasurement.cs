using InfluxDB.Client.Core;
using System;

namespace VisualizationService.Measurements
{
    [Measurement("temperature")]
    public class TemperatureMeasurement
    {
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }

        [Column("value")] public double Value { get; set; }
    }
}
