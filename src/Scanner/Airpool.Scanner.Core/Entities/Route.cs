using Airpool.Scanner.Core.Entities.Base;
using System;

namespace Airpool.Scanner.Core.Entities
{
    public class Route : Entity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid? StartLocationId { get; set; }
        public Location StartLocation { get; set; }
        public Guid? EndLocationId { get; set; }
        public Location EndLocation { get; set; }
    }
}
