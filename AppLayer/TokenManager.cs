using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace AppLayer
{
    public class TokenManager
    {
        public static string GenerateToken(string guid, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, guid),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.UserData, guid),
                new Claim(ClaimTypes.Role, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                ConfigurationManager.AppSettings["Jwt:Issuer"],
                ConfigurationManager.AppSettings["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}