namespace DataLayer.DTOs
{
    public class LapWriteDTO
    {
        public int RaceId { get; set; }

        public int DriverId { get; set; }

        public int Lap { get; set; }

        public int Position { get; set; }

        public string Time { get; set; }

        public int Milliseconds { get; set; }
    }
}
