using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbissalSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace AbissalSystem.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.GivenName, user.Name),
                new(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHanlder.CreateToken(tokenDescriptor);
        return tokenHanlder.WriteToken(token);
    }
    }
}