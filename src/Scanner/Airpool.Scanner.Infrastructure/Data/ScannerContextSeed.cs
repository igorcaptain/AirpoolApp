using Airpool.Scanner.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data
{
    public class ScannerContextSeed
    {
        public static async Task SeedAsync(ScannerContext scannerContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                scannerContext.Database.Migrate();

                if (!scannerContext.Locations.Any()) // if location table is empty -> seed database
                {
                    scannerContext.CabinClasses.AddRange(GetPreconfiguredCabinClasses());
                    await scannerContext.SaveChangesAsync();

                    scannerContext.Locations.AddRange(GetPreconfiguredLocations());
                    await scannerContext.SaveChangesAsync();

                    scannerContext.Flights.AddRange(await GetPreconfiguredFlights(scannerContext));
                    await scannerContext.SaveChangesAsync();

                    scannerContext.Tickets.AddRange(await GetPreconfiguredTickets(scannerContext));
                    await scannerContext.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ScannerContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(scannerContext, loggerFactory, retryForAvailability);
                }
            }
        }

        #region GetMockData

        private static IEnumerable<CabinClass> GetPreconfiguredCabinClasses()
        {
            return new List<CabinClass>()
            {
                new CabinClass() { Name = "Econom" }
            };
        }

        private static IEnumerable<Location> GetPreconfiguredLocations()
        {
            return new List<Location>()
            {
                new Location() { 
                    Name = "Boryspil International Airport (KBP)", 
                    AirportName = "Boryspil International Kyiv Airport", 
                    City = "Kyiv", 
                    IATA = "KBP", 
                    Country = "Ukraine", 
                    Latitude = 50.342613, 
                    Longitude = 30.892525 
                },
                new Location() { 
                    Name = "Warsaw Chopin Airport (WAW)", 
                    AirportName = "Warsaw Chopin Airport", 
                    City = "Warsaw", 
                    IATA = "WAW", 
                    Country = "Poland", 
                    Latitude = 52.165, 
                    Longitude = 20.968333 
                }
            };
        }

        private static async Task<IEnumerable<Flight>> GetPreconfiguredFlights(ScannerContext scannerContext)
        {
            IList<Location> locations = await scannerContext.Locations.ToListAsync();
            return new List<Flight>()
            {
                new Flight() 
                {
                    Name = "TestFlight#1337",
                    StartDateTime = DateTime.Now, 
                    EndDateTime = DateTime.Now.AddHours(3), 
                    StartLocationId = locations.Where(l => l.IATA == "KBP").FirstOrDefault().Id, 
                    EndLocationId = locations.Where(l => l.IATA == "WAW").FirstOrDefault().Id 
                }
            };
        }

        private static async Task<IEnumerable<Ticket>> GetPreconfiguredTickets(ScannerContext scannerContext)
        {
            IList<Flight> flights = await scannerContext.Flights.ToListAsync();
            return new List<Ticket>()
            {
                new Ticket()
                {
                    FlightId = flights.Where(f => f.Name == "TestFlight#1337").FirstOrDefault().Id,
                    CabinClassId = 1,
                    PassengerFirstName = "Valerii",
                    PassengerLastName = "Zhmyshenko",
                    PassengerBirthdate = DateTime.Now.AddYears(-54),
                    PassengerPassport = "000123456"
                }
            };
        }

        #endregion
    }
}
