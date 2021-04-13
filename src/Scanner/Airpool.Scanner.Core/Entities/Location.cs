using Airpool.Scanner.Core.Entities.Base;

namespace Airpool.Scanner.Core.Entities
{
    public class Location : Entity
    {
        public string Name { get; set; }
        public string AirportName { get; set; }
        public string TerminalName { get; set; }
        public string IATA { get; set; }
        // geo
        public string Country { get; set; }
        public string City { get; set; }
        // coordinates
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string CoordinateString { get; set; }
    }
}
