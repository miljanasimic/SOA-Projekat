using DataLayer.HelperClasses;

namespace DataLayer.DTOs
{
    public class CircuitsApiReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Competition { get; set; }

        public Location Location { get; set; }

        public int Laps { get; set; }

        public string Length { get; set; }
    }
}
