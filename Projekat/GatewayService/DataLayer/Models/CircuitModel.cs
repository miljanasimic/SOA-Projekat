using DataLayer.DTOs;
using DataLayer.HelperClasses;

namespace DataLayer.Models
{
    public class CircuitModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public Competition Competition { get; set; }

        public int Laps { get; set; }

        public string Length { get; set; }

        public string Race_distance { get; set; }

        public LapRecord Lap_record { get; set; }

        public CircuitsApiReadDTO ConvertToReadDTO()
        {
            return new CircuitsApiReadDTO
            {
                Id = Id,
                Name = Name,
                Image = Image,
                Competition = Competition.Name,
                Location = new Location()
                {
                    Country = Competition.Location.Country,
                    City = Competition.Location.City
                },
                Laps = Laps,
                Length = Length
            };
        }
    }
}
