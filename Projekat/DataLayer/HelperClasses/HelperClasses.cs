namespace DataLayer.HelperClasses
{
    public class Location
    {
        public string Country { get; set; }

        public string City { get; set; }
    }

    public class Competition 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }
    }

    public class LapRecord
    {
        public string Time { get; set; }

        public string Driver { get; set; }

        public string Year { get; set; }
    }
}
