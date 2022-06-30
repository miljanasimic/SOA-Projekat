using InfluxDB.Client.Core;
using System;

namespace VisualizationService.Measurements
{
    [Measurement("temeperature")]
    public class TemperatureMeasurement
    {
        [Column("origin", IsTag = true)] public long Origin { get; set; }

        [Column(IsTimestamp = true)] public DateTime Time { get; set; }

        [Column("value")] public double Value { get; set; }
    }
}
