namespace DataLayer.DTOs
{
    public class RaceWriteDTO
    {
        public int RaceId { get; set; }

        public int Year { get; set; }

        public int Round { get; set; }

        public int CircuitId { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Url { get; set; }
    }
}
