using Airpool.Scanner.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Core.Entities
{
    public class CabinClass : EntityBase<int>
    {
        public string Name { get; set; }
    }
}
