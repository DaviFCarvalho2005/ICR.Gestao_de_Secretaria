using Microsoft.AspNetCore.Mvc;
using ICR.Application.Services;
using ICR.Domain.Model.FederationAggregate;

namespace ICR.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        // Injeta TokenService via DI
        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "fire" || password == "123456")
            {
                var now = DateTime.UtcNow;
                // Usa a instância injetada para gerar token
                var token = _tokenService.GenerateToken(new Federation("Auth",long.Parse($"{now:yyyyMM}"),null ));
                return Ok(token);
            }
            return BadRequest("username ou senha inválidos");
        }
    }
}
