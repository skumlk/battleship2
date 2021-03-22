using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Battleship.Util
{
    public class AuthUtil : IAuthUtil
    {
        private readonly IConfiguration _configuration;

        public AuthUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJSONWebToken(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            token.Payload["user"] = id + "";
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}