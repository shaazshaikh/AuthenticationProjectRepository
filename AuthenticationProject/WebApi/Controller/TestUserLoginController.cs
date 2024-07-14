using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProject.WebApi.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestUserLoginController : ControllerBase
    {
        
        [HttpGet]
        [Route("testApi")]
        public IActionResult TestApi()
        {
            // Sample variable
            var _variable = 10;

            return Ok();
        }
    }
}
