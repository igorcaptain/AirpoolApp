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
}
