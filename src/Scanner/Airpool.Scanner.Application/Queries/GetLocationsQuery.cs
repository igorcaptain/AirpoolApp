using Airpool.Scanner.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Queries
{
    public class GetLocationsQuery : IRequest<List<LocationResponse>>
    {
    }
}
