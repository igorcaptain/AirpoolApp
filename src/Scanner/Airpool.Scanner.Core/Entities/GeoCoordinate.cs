using Airpool.Scanner.Core.Entities.Base;

namespace Airpool.Scanner.Core.Entities
{
    public class GeoCoordinate : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }
}
