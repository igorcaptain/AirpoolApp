using Airpool.Scanner.Application.Options;
using Airpool.Scanner.Application.Queries;
using Airpool.Scanner.Application.Responses;
using Airpool.Scanner.Core.Entities.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Airpool.Scanner.API.Controllers
{
    /// <summary>
    /// Scanner Controller, contains endpoints for scanner fuctionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScannerController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Controller constructor with dependency injection
        /// </summary>
        public ScannerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns a string "Ok" if server is alive
        /// </summary>
        /// <remarks>This endpoint can be used for server healthcheck. If server is alive will return "Ok" in JSON format result.</remarks>
        /// <response code="200">Returns string which contains server status message</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Healthcheck()
        {
            return Ok(await Task.Run(() => new JsonResult("Ok")));
        }

        /// <summary>
        /// Retrieves a specific flights by flight filter
        /// </summary>
        /// <remarks>This endpoint can be used for get flights from server by search option, temporary sended from client. Query parameter, that provide filtering for flights.<br />
        /// <b>OriginDateTime</b> - flight departure date, example: 2020-05-13;<br />
        /// <b>OriginLocationId</b> - flight departure id, See <see cref="GetLocations()"/> to get guid;<br />
        /// <b>DestinationLocationId</b> - flight arrival id, See <see cref="GetLocations()"/> to get guid;<br />
        /// <b>ReturnDateTime</b> (optional) - flight departure date from DestinationLocationId, represents return route, example: 2021-05-14.<br />
        /// </remarks>
        /// <response code="200">Returns FilterFlightResponse which contains fields "Departure" array, "Arrival" array and "IsOneWay" boolean</response>
        /// <param name="searchOption">
        /// Temporary query parameter, that provide filtering option.
        /// Dictionary: Default - 1; FromCache - 2; Greedy - 3.
        /// </param>
        [HttpGet]
        [Route("flights")]
        [ProducesResponseType(typeof(FilteredFlightResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindFlights([FromQuery] FlightFilter flightFilter, [FromQuery] SearchOption searchOption = SearchOption.Default)
        {
            var query = new GetFilteredFlightsQuery(flightFilter, searchOption);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of locations
        /// </summary>
        /// <remarks>This endpoint can be used for get list of locations available in database.</remarks>
        /// <response code="200">Returns LocationResponse, struct of JSON see below:</response>
        [HttpGet]
        [Route("locations")]
        [ProducesResponseType(typeof(LocationResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocations()
        {
            var query = new GetLocationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
