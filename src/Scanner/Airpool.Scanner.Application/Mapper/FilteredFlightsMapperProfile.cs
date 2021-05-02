using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Mapper
{
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
