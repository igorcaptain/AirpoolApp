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
        public async Task<IActionResult> FindFlights([FromQuery] FlightFilter flightFilter)
        {
            //flightFilter = new FlightFilter()
            //{
            //    OriginLocationId = Guid.Parse("B0C7540C-C2D8-452A-E7AC-08D90CEDEDC1"),
            //    OriginDateTime = DateTime.Parse("2021-05-15"),
            //    DestinationLocationId = Guid.Parse("39AF7DFF-C281-4709-E564-08D90CEDEDC1"),
            //    ReturnDateTime = DateTime.Parse("2021-05-16")
            //};
            var query = new GetFilteredFlightsQuery(flightFilter);
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
