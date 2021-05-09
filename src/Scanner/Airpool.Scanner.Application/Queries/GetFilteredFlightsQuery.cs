using Airpool.Scanner.Application.Options;
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
    public class GetFilteredFlightsQuery : IRequest<FilteredFlightResponse> 
    {
        public FlightFilter FlightFilter { get; }

        public SearchOption SearchOption { get; }

        public GetFilteredFlightsQuery(FlightFilter flightFilter, SearchOption searchOption)
        {
            FlightFilter = flightFilter;
            SearchOption = searchOption;
        }
    }
}
