using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.AuthenticationModels;

namespace AuthenticationProject.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpRepository _userSignUpRepository;
        public SignUpController(ISignUpRepository userSignUpRepository)
        {
            _userSignUpRepository = userSignUpRepository;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> signUpUser([FromBody] UserLoginModel model)
        {
            var isCreated = await _userSignUpRepository.CreateUserAccountAsync(model.UserName, model.Password);
            if (isCreated)
            {
                return StatusCode(StatusCodes.Status201Created, isCreated);
            }
            else
            {
                return Conflict(new { message = "username exists." });
            }
        }
    }
}
