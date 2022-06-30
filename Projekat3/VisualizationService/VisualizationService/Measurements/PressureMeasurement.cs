using InfluxDB.Client.Core;
using System;

namespace VisualizationService.Measurements
{
    [Measurement("pressure")]
    public class PressureMeasurement
    {
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }

        [Column("value")] public double Value { get; set; }
    }
}
