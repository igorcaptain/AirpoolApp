using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Airpool.Scanner.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScannerController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> HelloMessage()
        {
            return Ok(await Task.Run(() => "hello, world"));
        }
    }
}
