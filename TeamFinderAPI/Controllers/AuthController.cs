using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeamFinderAPI.Controllers.PostBody;

namespace TeamFinderAPI.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

       [HttpPost("login")]
       public IActionResult Login([FromBody] LoginBody body){
            return Ok();
       } 

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationBody body){
            return Ok();
        }

    }
}