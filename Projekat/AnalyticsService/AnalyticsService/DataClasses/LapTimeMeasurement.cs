using InfluxDB.Client.Core;

namespace AnalyticsService.DataClasses
{
    [Measurement("LapTimes")]
    public class LapTimeMeasurement
    {
        [Column("driverId", IsTag = true)] public int DriverId { get; set; }

        [Column("milliseconds")] public int Milliseconds { get; set; }
    }
}
