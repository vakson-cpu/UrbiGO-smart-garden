using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inf_Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Inf_api.Services.Auth
{
    public class TokenService : ITokenService
    {
        public IConfiguration _config { get; set; }
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerateToken(AppUser user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddHours(200),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        
    }
}