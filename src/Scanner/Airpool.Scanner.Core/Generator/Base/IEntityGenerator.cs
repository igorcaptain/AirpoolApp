using Airpool.Scanner.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Core.Generator.Base
{
    public interface IEntityGenerator<T, K> 
        where T : Entity 
        where K : Entity
    {
        T GenerateRandomEntity(IList<K> collection = null);
        IList<T> GenerateRandomEntities(IList<K> collection = null, int count = 1);
    }
}
