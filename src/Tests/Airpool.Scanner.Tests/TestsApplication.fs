module TestsApplication

open Xunit
open Airpool.Scanner.Application.Queries
open Moq
open MediatR
open Airpool.Scanner.Application.Responses
open Airpool.Scanner.Application.Options
open Airpool.Scanner.Core.Entities.Filters

[<Fact>]
let ``Is mediator response LocationResponse for GetLocationsQuery`` () =
    let fakeMediator = new Mock<IMediator>();
    let query = new GetLocationsQuery();
    fakeMediator.Setup(fun m -> m.Send(<@ It.IsAny<GetLocationsQuery>() @>)).ReturnsAsync(It.IsAny<LocationResponse>()) |> ignore;
    fakeMediator.Object.Send(query) |> ignore;
    fakeMediator.Verify(fun x -> x.Send(It.IsAny<GetLocationsQuery>()));

[<Fact>]
let ``Is mediator response FilteredFlightResponse for GetFilteredFlightsQuery`` () =
    let fakeMediator = new Mock<IMediator>();
    let query = new GetFilteredFlightsQuery(new FlightFilter(), SearchOption.Default);
    fakeMediator.Setup(fun m -> m.Send(It.IsAny<GetFilteredFlightsQuery>())).ReturnsAsync(It.IsAny<FilteredFlightResponse>()) |> ignore;
    fakeMediator.Object.Send(query) |> ignore;
    fakeMediator.Verify(fun x -> x.Send(It.IsAny<GetFilteredFlightsQuery>()));