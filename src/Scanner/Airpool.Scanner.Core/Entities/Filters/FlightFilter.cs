using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Core.Entities.Filters
{
    public class FlightFilter
    {
        public DateTime OriginDateTime { get; set; }
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public DateTime? ReturnDateTime { get; set; } // if null route is one-way
    }
}
