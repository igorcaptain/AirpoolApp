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
        T GenerateRandomEntity(IList<K> collection);
        T GenerateRandomEntity(IList<K> collection, DateTime dateSeed);
        Task<IList<T>> GenerateRandomEntities(IList<K> collection, int count = 1);
        Task<IList<T>> GenerateRandomEntities(IList<K> collection, DateTime dateSeed, int count = 1);
    }
}
