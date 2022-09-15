using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbissalSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace AbissalSystem.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, usuario.Id.ToString()),
                    new(ClaimTypes.GivenName, usuario.Nome),
                    new(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            return tokenHanlder.WriteToken(token);
        }

        public string RefreshToken(Usuario usuario)
        {
            return "'test'";
        }
    }
}