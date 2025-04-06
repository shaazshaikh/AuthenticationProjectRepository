using AuthenticationProject.Models.RequestModels;
using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> SignUpUser([FromBody] UserRequestModel model)
        {
            var isCreated = await _userSignUpRepository.CreateUserAccountAsync(model);
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
