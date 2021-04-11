using Airpool.Scanner.Core.Entities.Base;

namespace Airpool.Scanner.Core.Entities
{
    public class Location : Entity
    {
        public string Name { get; set; }
        public string AirportName { get; set; }
        public string TerminalName { get; set; }
        // geo
        public string Country { get; set; }
        public string City { get; set; }
        public GeoCoordinate Coordinate { get; set; }
    }
}
