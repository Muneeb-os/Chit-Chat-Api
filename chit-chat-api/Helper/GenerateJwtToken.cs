using chit_chat_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace chit_chat_api.Helper
{
    public class GenerateJwtToken
    {
        private readonly IConfiguration _configuration;
        public GenerateJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JwtToken(User user)
        {
            var jwtSetting = _configuration.GetSection("JWTSetting");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["SecurityKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.user_id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.user_email ?? ""),
        new Claim("userName", user.user_name ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
