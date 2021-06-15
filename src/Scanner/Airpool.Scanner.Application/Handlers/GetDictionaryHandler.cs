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
    public class GetDictionaryHandler : IRequestHandler<GetDictionaryQuery, List<DictionaryResponse>>
    {
        private readonly Dictionary<string, Type> dictionaryMap = new Dictionary<string, Type>()
        {
            {"cabinclass", typeof(CabinClass) },
            {"location", typeof(Location) }
        };

        private readonly IServiceProvider _serviceProvider;

        public GetDictionaryHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<DictionaryResponse>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
        {
            List<DictionaryResponse> result = new();

            var repositoryType = typeof(IRepository<,>).MakeGenericType(dictionaryMap[request.DictionaryName]);
            var repository = _serviceProvider.GetService(repositoryType);
            
            switch(repository)
            {
                case IRepository<Location, Guid> locationRepository:
                    result = (await locationRepository.GetAllAsync()).Select(l => new DictionaryResponse()
                    {
                        Id = l.Id.ToString(),
                        Name = l.Name
                    }).ToList();
                    break;
                default:
                    // log resolve instance error here
                    break;
            }

            return result;
        }
    }
}
