using InvestmentUseCase.Domain.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvestmentUseCase.Infra.Auth.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly int _expirationMinutes;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["Jwt:Secret"];
            _expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"]);
        }

        public string GenerateToken(Guid userId, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
