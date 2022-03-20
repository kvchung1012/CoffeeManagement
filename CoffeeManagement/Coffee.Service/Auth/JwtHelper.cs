using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Auth
{
    public class JwtHelper
    {
        public const string SecretKey = "chung1012@coffee.com.vn";
        public static string GenerateJwtToken(JwtPayloadModel payload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", payload.Id.ToString()),
                    new Claim("FullName", payload.FullName),
                    new Claim("Username", payload.UserName),
                    new Claim("Email", payload.Email??""),
                    new Claim("Phone", payload.Phone),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static JwtPayloadModel ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            token = token.TrimStart("Bearer".ToCharArray()).Trim();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
            var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;
            var phone = int.Parse(jwtToken.Claims.First(x => x.Type == "phone").Value);
            var email = int.Parse(jwtToken.Claims.First(x => x.Type == "email").Value);
            var payload = new JwtPayloadModel()
            {
                Id = long.Parse((jwtToken.Claims.First(x => x.Type == "Id").Value)),
                FullName = jwtToken.Claims.First(x => x.Type == "Fullname").Value,
                UserName = jwtToken.Claims.First(x => x.Type == "Username").Value,
                Phone = jwtToken.Claims.First(x => x.Type == "Phone").Value,
                Email = jwtToken.Claims.First(x => x.Type == "Email").Value,
            };
            return payload;
        }
    }
}
