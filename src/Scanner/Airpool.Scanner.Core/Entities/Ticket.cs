using Airpool.Scanner.Core.Entities.Base;
using System;

namespace Airpool.Scanner.Core.Entities
{
    public class Ticket : Entity
    {
        public Guid? FlightId { get; set; }
        public Flight Flight { get; set; }
        public int? CabinClassId { get; set; }
        public CabinClass CabinClass { get; set; }

        public string PassengerFirstName { get; set; }
        public string PassengerLastName { get; set; }
        public DateTime PassengerBirthdate { get; set; }
        public string PassengerPassport { get; set; }
    }
}
