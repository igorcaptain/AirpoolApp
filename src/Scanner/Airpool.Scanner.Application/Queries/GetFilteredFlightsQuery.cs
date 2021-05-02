using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Queries
{
    public class GetFilteredFlightsQuery : IRequest<List<FilteredFlightResponse>> 
    {
        public FlightFilter FlightFilter { get; }

        public GetFilteredFlightsQuery(FlightFilter flightFilter)
        {
            FlightFilter = flightFilter;
        }
    }
}
