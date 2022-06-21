using DataLayer.HelperClasses;
using System.Collections.Generic;

namespace DataLayer.DTOs
{
    public class DriverApiReadDTO
    {
        public string Name { get; set; }

        public string Abbr { get; set; }

        public string Image { get; set; }

        public string Nationality { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        public int Podiums { get; set; }

        public string CareerPoints { get; set; }

        public List<TeamStruct> Teams { get; set; }
    }
}
