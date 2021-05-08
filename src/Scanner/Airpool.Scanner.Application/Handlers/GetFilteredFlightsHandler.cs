using Airpool.Scanner.Application.Mapper;
using Airpool.Scanner.Application.Queries;
using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Repository.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Handlers
{
    public class GetFilteredFlightsHandler : IRequestHandler<GetFilteredFlightsQuery, FilteredFlightResponse>
    {
        private readonly IRepository<Flight> _flightsRepository;

        public GetFilteredFlightsHandler(IRepository<Flight> flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<FilteredFlightResponse> Handle(GetFilteredFlightsQuery request, CancellationToken cancellationToken)
        {
            FilteredFlightResponse result;
            var filter = request.FlightFilter;

            var filteredDepartureFlights = await _flightsRepository.GetAsync(f =>
                f.StartLocationId == filter.OriginLocationId &&
                f.EndLocationId == filter.DestinationLocationId &&
                f.StartDateTime.Date == filter.OriginDateTime.Date,
                null,
                true,
                with => with.StartLocation, with => with.EndLocation);

            if (filter.ReturnDateTime != null)
            {
                var filteredArrivalFlights = await _flightsRepository.GetAsync(f =>
                    f.StartLocationId == filter.DestinationLocationId &&
                    f.EndLocationId == filter.OriginLocationId &&
                    f.StartDateTime.Date == filter.ReturnDateTime.Value.Date,
                    null,
                    true,
                    with => with.StartLocation, with => with.EndLocation);

                result = new FilteredFlightResponse()
                {
                    Departure = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredDepartureFlights),
                    Arrival = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredArrivalFlights)
                };
            }
            else
            {
                var test = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredDepartureFlights);

                result = new FilteredFlightResponse()
                {
                    Departure = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredDepartureFlights),
                    Arrival = null
                };
            }
            
            return result;
        }
    }
}
