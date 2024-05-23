using Microsoft.IdentityModel.Tokens;
using SharedModels.AuthenticationModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationProject.Helpers
{
    public class AuthenticationHelper
    {
        public IConfiguration Configuration { get; }
        public AuthenticationHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private string GenerateToken(UserLoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            { new Claim(ClaimTypes.NameIdentifier, user.UserName) };
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, signingCredentials:  credentials, expires: DateTime.Now.AddMinutes(20));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
