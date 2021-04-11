using Airpool.Scanner.Core.Entities.Base;
using System;

namespace Airpool.Scanner.Core.Entities
{
    public class Ticket : Entity
    {
        public Guid FlightId { get; set; }
        public string Flight { get; set; }
        public string PassengerFirstName { get; set; }
        public string PassengerLastName { get; set; }
        public DateTime PassengerBirthdate { get; set; }
        public string PassengerPassport { get; set; }
    }
}
