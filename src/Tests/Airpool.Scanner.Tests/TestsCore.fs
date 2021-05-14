module TestsCore

open System
open Xunit
open Airpool.Scanner.Core.Generator
open Airpool.Scanner.Core.Entities

let entityGenerator = new FlightGenerator();

let getLocations() = 
    let locations = new System.Collections.Generic.List<Location>();
    locations.Add(new Location(AirportName = "Boryspil International Airport", IATA = "KBP", Country = "Ukraine", City = "Kyiv", Latitude = 50.341244, Longitude = 30.895206));
    locations.Add(new Location(AirportName = "Wroclaw Airport", IATA = "WRO", Country = "Poland", City = "Wroclaw", Latitude = 51.10482, Longitude = 16.899403));
    locations

[<Fact>]
let ``Get one random entity from entity generator`` () =
    let locations = getLocations()
    let entity = entityGenerator.GenerateRandomEntity(locations, DateTime.Now)
    Assert.NotNull(entity);

[<Fact>]
let ``Check correct date of generated entity`` () =
    let locations = getLocations()
    let currentDate = DateTime.Now
    let entity = entityGenerator.GenerateRandomEntity(locations, currentDate)
    Assert.Equal(currentDate.Date, entity.StartDateTime.Date)

[<Fact>]
let ``Check origin and destination locations of generated entity`` () =
    let locations = getLocations()
    let currentDate = DateTime.Now
    let entity = entityGenerator.GenerateRandomEntity(locations, currentDate)
    Assert.True(not (entity.StartLocation.Equals(entity.EndLocation)) && locations.Contains(entity.StartLocation) && locations.Contains(entity.EndLocation))

[<Fact>]
let ``Get 10 random entities from entity generator`` () =
    let locations = getLocations()
    let entities = 
        entityGenerator.GenerateRandomEntities(locations, DateTime.Now, 10) 
        |> Async.AwaitTask 
        |> Async.RunSynchronously
    Assert.Equal(entities.Count, 10);

[<Fact>]
let ``Get 100 random entities from entity generator`` () =
    let locations = getLocations()
    let entities = 
        entityGenerator.GenerateRandomEntities(locations, DateTime.Now, 100) 
        |> Async.AwaitTask 
        |> Async.RunSynchronously
    Assert.Equal(entities.Count, 100);