using DataLayer.DTOs;
using DataLayer.HelperClasses;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class DriverModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public string Image { get; set; }

        public string Nationality { get; set; }

        public Country Country { get; set; }

        public string Birthdate { get; set; }

        public string Birthplace { get; set; }

        public int Number { get; set; }

        public int Podiums { get; set; }

        public string Career_points { get; set; }

        public virtual IList<TeamStruct> Teams { get; set; }

        public DriverApiReadDTO ConvertToReadDTO()
        {
            var teams = new List<TeamStruct>();
            foreach(var team in Teams)
            {
                teams.Add(team);
            }

            return new DriverApiReadDTO
            {
                Name = Name,
                Abbr = Abbr,
                Image = Image,
                Nationality = Nationality,
                Country = Country.Name,
                Number = Number,
                Podiums = Podiums,
                CareerPoints = Career_points,
                Teams = teams
            };
        }
    }
}
