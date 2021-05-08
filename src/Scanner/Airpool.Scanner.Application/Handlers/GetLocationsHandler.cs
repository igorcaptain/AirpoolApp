using Airpool.Scanner.Application.Queries;
using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Repository.Base;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Handlers
{
    public class GetLocationsHandler : IRequestHandler<GetLocationsQuery, List<LocationResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Location> _locationRepository;

        public GetLocationsHandler(IMapper mapper, IRepository<Location> locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<List<LocationResponse>> Handle(GetLocationsQuery request, CancellationToken cancellationToken) => 
            _mapper.Map<List<LocationResponse>>(await _locationRepository.GetAllAsync());
    }
}
