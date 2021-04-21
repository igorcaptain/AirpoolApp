using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data.ThirdParty.TravelPayouts.Entities
{
    public class TravelPayoutsLocation
    {
        public string Name { get; set; }
        public string AirportName { get; set; }
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
