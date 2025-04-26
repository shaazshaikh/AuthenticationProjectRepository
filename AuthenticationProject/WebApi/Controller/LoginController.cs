using AuthenticationProject.Helpers;
using AuthenticationProject.Models.RequestModels;
using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AuthenticateUser([FromBody] UserRequestModel model)
        {
            var user = await _userLoginRepository.AuthenticateUserAsync(model);
            if(user != null)
            {
                var token = await _authenticationHelper.GenerateToken(user);
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("getClientToken")]
        public async Task<IActionResult> GetClientToken(ClientRequestModel model)
        {
            var isCorrectMicroservice = await _authenticationHelper.CheckMicroservice(model);
            if(isCorrectMicroservice)
            {
                var token = await _authenticationHelper.GenerateClientToken(model);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
            
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
