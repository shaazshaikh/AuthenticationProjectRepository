using AuthenticationProject.Models.RequestModels;
using AuthenticationProject.Models.ResponseModels;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<bool> CheckMicroservice(ClientRequestModel model)
        {
            var clients = Configuration.GetSection("Clients").Get<Dictionary<string, string>>();
            if (!clients.ContainsKey(model.ClientId) || clients[model.ClientId] != model.ClientSecret)
            {
                return await Task.FromResult(false);
            }
            else
            {
                return await Task.FromResult(true);
            }

        }

        public async Task<string> GenerateClientToken(ClientRequestModel model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim("ClientId", model.ClientId) };
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, signingCredentials: credentials, expires: DateTime.UtcNow.AddMinutes(120));
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<string> GenerateToken(UserResponseModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.UserName)};
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, signingCredentials:  credentials, expires: DateTime.UtcNow.AddMinutes(120));
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
