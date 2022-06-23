using DataLayer.DTOs;
using DataLayer.HelperClasses;

namespace DataLayer.Models
{
    public class RaceModel
    {
        public int Id { get; set; }

        public Competition Competition { get; set; }

        public Circuit Circuit { get; set; }

        public int Season { get; set; }

        public string Type { get; set; }

        public string Distance { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public RaceApiReadDTO ConvertToReadDTO()
        {
            return new RaceApiReadDTO()
            {
                Competition = Competition,
                Circuit = Circuit,
                Season = Season,
                Type = Type,
                Distance = Distance,
                Status = Status
            };
        }
    }
}
