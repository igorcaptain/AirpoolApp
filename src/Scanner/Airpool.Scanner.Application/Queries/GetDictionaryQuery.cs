using Airpool.Scanner.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Application.Queries
{
    public class GetDictionaryQuery : IRequest<List<DictionaryResponse>>
    {
        public string DictionaryName { get; }

        public GetDictionaryQuery(string dictionaryName)
        {
            DictionaryName = dictionaryName.ToLower();
        }
    }
}
