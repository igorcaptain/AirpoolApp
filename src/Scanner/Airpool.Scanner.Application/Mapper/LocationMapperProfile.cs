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
    public class LocationMapperProfile : Profile
    {
        public LocationMapperProfile()
        {
            CreateMap<Location, LocationResponse>();
        }
    }
}
