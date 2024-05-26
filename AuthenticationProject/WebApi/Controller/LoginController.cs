using AuthenticationProject.Helpers;
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
        private readonly AuthenticationHelper _authenticationHelper;
        public LoginController(ILoginRepository userLoginRepository, AuthenticationHelper authenticationHelper)
        {
            _userLoginRepository = userLoginRepository;
            _authenticationHelper = authenticationHelper;
        }

        //[AllowAnonymous]
        [HttpPost]
        [Route("authenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginModel model)
        {
            var user = await _userLoginRepository.AuthenticateUserAsync(model.UserName, model.Password);
            if(user != null)
            {
                var token = _authenticationHelper.GenerateToken(user); //Testing purpose 2
                return Ok(token);
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
