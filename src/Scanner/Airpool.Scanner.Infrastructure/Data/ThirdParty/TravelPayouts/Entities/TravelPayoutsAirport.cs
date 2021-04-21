using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data.ThirdParty.TravelPayouts.Entities
{
    public class TravelPayoutsAirport
    {
        public string Code { get; set; }
        public string City_code { get; set; }
        public string Country_code { get; set; }
        public string Name { get; set; }
        public Coordinate Coordinates { get; set; }
        public string IATA_type { get; set; }
        public bool Flightable { get; set; }

        public class Coordinate
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }
    }
}
