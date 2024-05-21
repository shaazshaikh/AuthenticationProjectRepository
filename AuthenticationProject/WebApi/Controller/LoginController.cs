using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.AuthenticationModels;

namespace AuthenticationProject.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _userLoginRepository;
        public LoginController(ILoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        //public IActionResult Login([FromBody] UserLoginModel model)
        //{
        //    IActionResult response = Unauthorized();
        //}

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userLoginRepository.GetUsersAsync();
            return Ok(user);
        }
    }
}
