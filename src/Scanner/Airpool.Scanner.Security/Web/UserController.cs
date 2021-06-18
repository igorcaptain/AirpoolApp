using Airpool.Scanner.Security.Contract;
using Airpool.Scanner.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airpool.Scanner.Security.Web
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            return Ok(await _userService.RegisterUser(user));
        }
    }
}
