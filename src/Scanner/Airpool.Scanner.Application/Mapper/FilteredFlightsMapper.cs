using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities;
using AutoMapper;
using System;

namespace Airpool.Scanner.Application.Mapper
{
    // TODO: make it non-static and with DI
    public static class FilteredFlightsMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<FilteredFlightsMapperProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class FilteredFlightsMapperProfile : Profile
    {
        public FilteredFlightsMapperProfile()
        {
            CreateMap<Flight, FlightResponse>()
                .ForMember("StartLocationAirportName", opt => opt.MapFrom(x => x.StartLocation.AirportName))
                .ForMember("StartLocationIATA", opt => opt.MapFrom(x => x.StartLocation.IATA))
                .ForMember("StartLocationCountry", opt => opt.MapFrom(x => x.StartLocation.Country))
                .ForMember("StartLocationCity", opt => opt.MapFrom(x => x.StartLocation.City))

                .ForMember("EndLocationAirportName", opt => opt.MapFrom(x => x.EndLocation.AirportName))
                .ForMember("EndLocationIATA", opt => opt.MapFrom(x => x.EndLocation.IATA))
                .ForMember("EndLocationCountry", opt => opt.MapFrom(x => x.EndLocation.Country))
                .ForMember("EndLocationCity", opt => opt.MapFrom(x => x.EndLocation.City));
        }
    }
}
