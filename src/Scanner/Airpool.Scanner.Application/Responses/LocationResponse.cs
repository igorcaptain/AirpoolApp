using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Responses
{
    public class LocationResponse
    {
        public Guid Id { get; set; }
        public string AirportName { get; set; }
        public string IATA { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
