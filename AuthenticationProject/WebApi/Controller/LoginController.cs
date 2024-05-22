using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginModel model)
        {
            var user = await _userLoginRepository.AuthenticateUserAsync(model.UserName, model.Password);
            if(user != null)
            {
                //var token = GenerateToken(user); //Testing purpose
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userLoginRepository.GetUsersAsync();
            return Ok(user);
        }
    }
}
