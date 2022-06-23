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

    public class Country
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class TeamStruct
    {
        public int Season { get; set; }

        public TeamData Team { get; set; }
    }

    public class TeamData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
    }

    public class Circuit
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}
