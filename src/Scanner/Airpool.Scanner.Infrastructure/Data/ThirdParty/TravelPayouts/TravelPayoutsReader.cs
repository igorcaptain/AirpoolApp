using Airpool.Scanner.Infrastructure.Data.ThirdParty.TravelPayouts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data.ThirdParty.TravelPayouts
{
    public class TravelPayoutsReader
    {
        private static readonly string _baseApiAddress = @"https://api.travelpayouts.com/data/en/";
        private static readonly string _airportsAddress = _baseApiAddress + "airports.json";
        private static readonly string _countriesAddress = _baseApiAddress + "countries.json";
        private static readonly string _citiesAddress = _baseApiAddress + "cities.json";

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private async Task<T> GetEntities<T>(string address)
        {
            using WebClient client = new();
            var result = await client.DownloadStringTaskAsync(address);
            return JsonSerializer.Deserialize<T>(result, _options);
        }

        public async Task<IList<TravelPayoutsAirport>> GetAirports() => 
            (await GetEntities<TravelPayoutsAirport[]>(_airportsAddress))
                .Where(a => a.IATA_type == "airport" && a.Flightable)
                .ToList();

        public async Task<IList<TravelPayoutsCountry>> GetCountries() => await GetEntities<TravelPayoutsCountry[]>(_countriesAddress);

        public async Task<IList<TravelPayoutsCity>> GetCities() => await GetEntities<TravelPayoutsCity[]>(_citiesAddress);

        public async Task<(IList<TravelPayoutsAirport> Airports, IList<TravelPayoutsCountry> Countries, IList<TravelPayoutsCity> Cities)> GetAll()
        {
            var airports = await GetAirports();
            var countries = (await GetCountries())
                .Where(c => airports.Select(a => a.Country_code).Contains(c.Code))
                .ToList();
            var cities = (await GetCities())
                .Where(c => countries.Select(x => x.Code).Contains(c.Country_code))
                .ToList();
            return (airports, countries, cities);
        }

        public async Task<IList<TravelPayoutsLocation>> GetLocations()
        {
            var data = await GetAll();
            return data.Airports
                .Select(a => 
                new TravelPayoutsLocation()
                {
                    AirportName = a.Name,
                    IATA = a.Code,
                    Country = data.Countries.Where(c => c.Code == a.Country_code).FirstOrDefault()?.Name,
                    City = data.Cities.Where(c => c.Code == a.City_code).FirstOrDefault()?.Name,
                    Latitude = a.Coordinates.lat,
                    Longitude = a.Coordinates.lon
                })
                .Where(l => !string.IsNullOrEmpty(l.Country) && !string.IsNullOrEmpty(l.City))
                .ToList();
        }
    }
}
