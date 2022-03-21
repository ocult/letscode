#nullable disable
using LetsCode.Customs;
using LetsCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsCode.Controllers;

[Route("login")]
[ApiController]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    public string privateKey = MyJwtConstants.DEFAULT_KEY;

    public LoginController(IConfiguration configuration)
    {
        privateKey = configuration[MyJwtConstants.CONFIG] ?? MyJwtConstants.DEFAULT_KEY;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationRequest model)
    {
        var user = UserRepository.Get(model.Login, model.Senha);

        if (user == null)
        {
            return NotFound(new { message = "Usuário ou senha inválidos" });
        }

        var token = JwtTokenService.GenerateToken(user, privateKey);

        return token;
    }
}
