using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Generator.Base;
using Airpool.Scanner.Infrastructure.Data.ThirdParty.TravelPayouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data
{
    public class ScannerContextSeed
    {
        public static async Task SeedAsync(ScannerContext scannerContext, ILoggerFactory loggerFactory, IEntityGenerator<Flight, Location> entityGenerator, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                if (!scannerContext.CabinClasses.Any())
                {
                    scannerContext.CabinClasses.AddRange(GetPreconfiguredCabinClasses());
                    await scannerContext.SaveChangesAsync();
                }

                if (!scannerContext.Locations.Any())
                {
                    scannerContext.Locations.AddRange(await GetLocationsFromApi());
                    await scannerContext.SaveChangesAsync();
                }

                if (!scannerContext.Flights.Any())
                {
                    scannerContext.Flights.AddRange(await entityGenerator.GenerateRandomEntities(await scannerContext.Locations.ToListAsync(), 500000));
                    await scannerContext.SaveChangesAsync();
                }

                if (!scannerContext.Tickets.Any())
                {
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
                    await SeedAsync(scannerContext, loggerFactory, entityGenerator, retryForAvailability);
                }
            }
        }

        #region GetMockData

        private static IEnumerable<CabinClass> GetPreconfiguredCabinClasses()
        {
            return new List<CabinClass>()
            {
                new CabinClass() { Name = "Economy" },
                new CabinClass() { Name = "Business" },
                new CabinClass() { Name = "First" }
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
            return new List<Ticket>()
            {
                new Ticket()
                {
                    FlightId = scannerContext.Flights.Include(f => f.StartLocation).Where(f => f.StartLocation.Country == "Ukraine").FirstOrDefault().Id,
                    CabinClassId = 1,
                    PassengerFirstName = "Valerii",
                    PassengerLastName = "Zhmyshenko",
                    PassengerBirthdate = DateTime.Now.AddYears(-54),
                    PassengerPassport = "000123456"
                }
            };
        }

        #endregion

        #region GetThirdPartyData
        private static async Task<IEnumerable<Location>> GetLocationsFromApi() =>
            (await (new TravelPayoutsReader()).GetLocations())
            .Select(tl => new Location() 
            {
                AirportName = tl.AirportName,
                IATA = tl.IATA,
                Country = tl.Country,
                City = tl.City,
                Latitude = tl.Latitude,
                Longitude = tl.Longitude
            });
        #endregion
    }
}
