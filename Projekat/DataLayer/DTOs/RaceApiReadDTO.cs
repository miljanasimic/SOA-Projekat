using DataLayer.HelperClasses;

namespace DataLayer.DTOs
{
    public class RaceApiReadDTO
    {
        public Competition Competition { get; set; }

        public Circuit Circuit { get; set; }

        public int Season { get; set; }

        public string Type { get; set; }

        public string Distance { get; set; }

        public string Status { get; set; }
    }
}
