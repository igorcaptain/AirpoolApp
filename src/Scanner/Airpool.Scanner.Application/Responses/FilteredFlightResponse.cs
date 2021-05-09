using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Responses
{
    public class FilteredFlightResponse
    {
        public List<FlightResponse> Departure { get; set; }
        public List<FlightResponse> Arrival { get; set; } // null if one way
        public bool IsOneWay 
        { 
            get => Arrival == null || Arrival?.Count == 0; 
        }
    }
}
