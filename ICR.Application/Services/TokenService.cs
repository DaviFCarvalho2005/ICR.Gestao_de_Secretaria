using Microsoft.IdentityModel.Tokens;
using ICR.Domain.Model.FederationAggregate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ICR.Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        // IConfiguration será injetado pelo projeto API
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object GenerateToken(Federation federation)
        {
            if (federation == null) throw new ArgumentNullException(nameof(federation));

            // Tenta pegar a chave da variável de ambiente
            var secret = Environment.GetEnvironmentVariable("JWT_KEY");

            // Se não existir, pega do appsettings.json da API via IConfiguration
            if (string.IsNullOrEmpty(secret))
            {
                secret = _configuration["JWT_KEY"];
                if (string.IsNullOrEmpty(secret))
                    throw new InvalidOperationException("JWT secret not configured in environment variables or appsettings.json.");
            }

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("federationId", federation.Id.ToString()),
                    new Claim("federationName", federation.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                Token = $"Bearer {tokenString}"
            };
        }
    }
}
