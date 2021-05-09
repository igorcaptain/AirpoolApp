using Airpool.Scanner.Application.Mapper;
using Airpool.Scanner.Application.Queries;
using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Entities.Filters;
using Airpool.Scanner.Core.Generator.Base;
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
        private readonly IEntityGenerator<Flight, Location> _entityGenerator;
        private readonly IRepository<Location> _locationsRepository;

        public GetFilteredFlightsHandler(
            IRepository<Flight> flightsRepository, 
            IRepository<Location> locationsRepository, 
            IEntityGenerator<Flight, Location> entityGenerator)
        {
            _flightsRepository = flightsRepository;
            _entityGenerator = entityGenerator;
            _locationsRepository = locationsRepository;
        }

        public async Task<FilteredFlightResponse> Handle(GetFilteredFlightsQuery request, CancellationToken cancellationToken)
        {
            FilteredFlightResponse result = new FilteredFlightResponse();
            switch (request.SearchOption)
            {
                case Options.SearchOption.Default: 
                    result = await GetFilteredFlightResponseDefault(request.FlightFilter);
                    break;
                case Options.SearchOption.FromCache:
                    result = await GetFilteredFlightResponseFromCache(request.FlightFilter);
                    break;
                case Options.SearchOption.Greedy: 
                    break;
            }
            return result;
        }

        private async Task<FilteredFlightResponse> GetFilteredFlightResponseDefault(FlightFilter filter)
        {
            FilteredFlightResponse result;

            var locations = (await _locationsRepository.GetAsync(location => 
                location.Id == filter.OriginLocationId || 
                location.Id == filter.DestinationLocationId)).ToList();

            var generatedDepartureEntities = (await _entityGenerator.GenerateRandomEntities(locations, filter.OriginDateTime, 10))
                    .Where(f => f.StartLocationId == filter.OriginLocationId);
            var filteredDepartureFlights = generatedDepartureEntities.Where(e => e.StartLocationId == filter.OriginLocationId)
                .Select(f =>
                {
                    f.StartLocation = locations.Where(l => l.Id == f.StartLocationId).FirstOrDefault();
                    f.EndLocation = locations.Where(l => l.Id == f.EndLocationId).FirstOrDefault();
                    return f;
                });

            if (filter.ReturnDateTime != null)
            {
                var generatedArrivalEntities = (await _entityGenerator.GenerateRandomEntities(locations, ((DateTime)filter.ReturnDateTime), 10))
                    .Where(f => f.StartLocationId == filter.OriginLocationId);
                var filteredArrivalFlights = generatedArrivalEntities.Where(e => e.EndLocationId == filter.DestinationLocationId)
                .Select(f =>
                {
                    f.StartLocation = locations.Where(l => l.Id == f.EndLocationId).FirstOrDefault();
                    f.EndLocation = locations.Where(l => l.Id == f.StartLocationId).FirstOrDefault();
                    return f;
                });

                result = new FilteredFlightResponse()
                {
                    Departure = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredDepartureFlights),
                    Arrival = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredArrivalFlights)
                };
            }
            else
            {
                result = new FilteredFlightResponse()
                {
                    Departure = FilteredFlightsMapper.Mapper.Map<List<FlightResponse>>(filteredDepartureFlights),
                    Arrival = null
                };
            }

            return result;
        }

        private async Task<FilteredFlightResponse> GetFilteredFlightResponseFromCache(FlightFilter filter)
        {
            FilteredFlightResponse result;

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
