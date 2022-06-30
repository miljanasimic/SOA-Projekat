using InfluxDB.Client.Core;
using System;

namespace VisualizationService.Measurements
{
    [Measurement("humidity")]
    public class HumidityMeasurement
    {
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }

        [Column("value")] public double Value { get; set; }
    }
}
