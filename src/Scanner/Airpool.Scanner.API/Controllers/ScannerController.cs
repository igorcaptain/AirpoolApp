using Airpool.Scanner.Application.Options;
using Airpool.Scanner.Application.Queries;
using Airpool.Scanner.Core.Entities.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Airpool.Scanner.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScannerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScannerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Healthcheck()
        {
            return Ok(await Task.Run(() => new JsonResult("Ok")));
        }

        [HttpGet]
        [Route("flights")]
        public async Task<IActionResult> FindFlights([FromQuery] FlightFilter flightFilter, [FromQuery] SearchOption searchOption = SearchOption.Default)
        {
            var query = new GetFilteredFlightsQuery(flightFilter, searchOption);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var query = new GetLocationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
