using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Generator.Base;
using Airpool.Scanner.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data.Generator
{

    internal record GeoPoint
    {
        private const double EARTH_RADIUS = 6372795;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double DistanceTo(GeoPoint point)
        {
            return GetDistance(this.Latitude, this.Longitude, point.Latitude, point.Longitude);
        }

        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = lat1 * Math.PI / 180;
            lon1 = lon1 * Math.PI / 180;
            lat2 = lat2 * Math.PI / 180;
            lon2 = lon2 * Math.PI / 180;

            var clat1 = Math.Cos(lat1);
            var clat2 = Math.Cos(lat2);
            var slat1 = Math.Sin(lat1);
            var slat2 = Math.Sin(lat2);
            var delta = lon2 - lon1;
            var cdelta = Math.Cos(delta);
            var sdelta = Math.Sin(delta);

            var y = Math.Sqrt(Math.Pow(clat2 * sdelta, 2) + Math.Pow(clat1 * slat2 - slat1 * clat2 * cdelta, 2));
            var x = slat1 * slat2 + clat1 * clat2 * cdelta;

            var atdelta = Math.Atan2(y, x);
            var distance = atdelta * EARTH_RADIUS;

            return distance;
        }
    }

    internal record Rectangle
    {
        public double Left { get; set; } // lon
        public double Top { get; set; } // lat
        public double Right { get; set; } // lon
        public double Bottom { get; set; } // lat

        public bool isInclude(double? latitude, double? longitude)
        {
            return latitude >= Bottom
                && latitude <= Top
                && longitude >= Left
                && longitude <= Right;
        }
    }

    internal static class Options
    {
        public static Rectangle FlightableRect { get; set; } = new Rectangle()
        {
            Left = -17.957694,
            Top = 65.253923,
            Right = 47.652655,
            Bottom = 24.361057
        };

        public static class Boeing767
        {
            // boeing-767
            // speed: 873 km/h
            // range: 4000 km
            // 252 passengers

            public static int Speed { get; } = 873;
            public static double Range { get; } = 4000 * 1000;
            public static int SeatsCount { get; set; } = 252;
        }
    }

    public class FlightGenerator : IEntityGenerator<Flight, Location>
    {
        public IList<Flight> GenerateRandomEntities(IList<Location> locations, int count = 1)
        {
            List<Flight> flights = new();
            int attempt = 0;

            while (flights.Count() < count && attempt <= 100)
            {
                var flight = GenerateRandomEntity(locations);
                if (!flights.Contains(flight))
                {
                    flights.Add(flight);
                    attempt = 0;
                }
                else
                    attempt++;
            }

            return flights;
        }

        public Flight GenerateRandomEntity(IList<Location> locations)
        {
            var filteredLocations = locations.Where(l => Options.FlightableRect.isInclude(l.Latitude, l.Longitude));
            
            Random random = new Random();
            int attempt = 0;
            Location location1, location2;

            while (true)
            {
                location1 = filteredLocations.ElementAt(random.Next(0, filteredLocations.Count()));
                location2 = filteredLocations.ElementAt(random.Next(0, filteredLocations.Count()));

                if (ValidateLocations(location1, location2) || attempt++ >= 20)
                    break;
            }

            DateTime startDate = DateTime.Now
                .AddDays(random.Next(1, 30))
                .AddHours(random.Next(0, 24))
                .AddMinutes(random.Next(0, 60));
            var distanceKm = GeoPoint.GetDistance((double)location1.Latitude, (double)location1.Longitude, (double)location2.Latitude, (double)location2.Longitude) / 1000;


            var flight = new Flight()
            {
                Name = location1.IATA + "-" + location2.IATA,
                StartDateTime = startDate,
                EndDateTime = startDate.AddHours(Options.Boeing767.Speed / distanceKm * 1.4),
                StartLocationId = location1.Id,
                EndLocationId = location2.Id
            };
            

            return flight;
        }

        private bool ValidateLocations(Location location1, Location location2)
        {
            var distance = GeoPoint.GetDistance((double)location1.Latitude, (double)location1.Longitude, (double)location2.Latitude, (double)location2.Longitude);
            double maxDistance = Options.Boeing767.Range;
            bool isValid = location1 != location2 && distance <= maxDistance;
            return isValid;
        }
    }
}
