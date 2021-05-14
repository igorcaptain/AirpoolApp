module TestsAPI

open Xunit
open Airpool.Scanner.API.Controllers
open Moq
open MediatR
open Airpool.Scanner.Application.Responses
open Airpool.Scanner.Application.Queries
open Microsoft.AspNetCore.Mvc
open Airpool.Scanner.Core.Entities.Filters

let castAs<'T when 'T : null> (o: obj) = 
    match o with
    | :? 'T as res -> res
    | _ -> null

[<Fact>]
let ``Check Healthcheck() controller endpoint`` () =
    let fakeMediator = new Mock<IMediator>();
    let scannerController = new ScannerController(fakeMediator.Object);
    let resultString = 
        (((scannerController.Healthcheck()
        |> Async.AwaitTask
        |> Async.RunSynchronously).Result
        |> castAs<OkObjectResult>).Value
        |> castAs<JsonResult>).Value.ToString()
    Assert.Equal("Ok", resultString);

[<Fact>]
let ``Check GetLocations() controller endpoint`` () =
    let fakeMediator = new Mock<IMediator>();
    fakeMediator.Setup(fun m -> m.Send(<@ It.IsAny<GetLocationsQuery>() @>)).ReturnsAsync(It.IsAny<LocationResponse>()) |> ignore;
    let scannerController = new ScannerController(fakeMediator.Object);
    let result = 
        scannerController.GetLocations() 
        |> Async.AwaitTask 
        |> Async.RunSynchronously;
    Assert.IsType<OkObjectResult>(result);


[<Fact>]
let ``Check FindFlights() controller endpoint`` () =
    let fakeMediator = new Mock<IMediator>();
    fakeMediator.Setup(fun m -> m.Send(<@ It.IsAny<GetLocationsQuery>() @>)).ReturnsAsync(It.IsAny<LocationResponse>()) |> ignore;
    let scannerController = new ScannerController(fakeMediator.Object);
    let result = 
        new FlightFilter()
        |> scannerController.FindFlights
        |> Async.AwaitTask 
        |> Async.RunSynchronously;
    Assert.IsType<OkObjectResult>(result);