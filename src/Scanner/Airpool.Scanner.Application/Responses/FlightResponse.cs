using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Responses
{
    public class FlightResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Guid? StartLocationId { get; set; }
        //public Location StartLocation { get; set; }
        //public string StartLocationName { get; set; }
        public string StartLocationAirportName { get; set; }
        public string StartLocationIATA { get; set; }
        public string StartLocationCountry { get; set; }
        public string StartLocationCity { get; set; }

        public Guid? EndLocationId { get; set; }
        //public Location EndLocation { get; set; }
        //public string EndLocationName { get; set; }
        public string EndLocationAirportName { get; set; }
        public string EndLocationIATA { get; set; }
        public string EndLocationCountry { get; set; }
        public string EndLocationCity { get; set; }
    }
}
