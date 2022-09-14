using AbissalSystem.Data;
using AbissalSystem.Extensions;
using AbissalSystem.Models;
using AbissalSystem.ResultViewModel;
using AbissalSystem.Services;
using AbissalSystem.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace AbissalSystem.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("/auth/signup")]
    public async Task<IActionResult> Post(
        [FromBody] RegisterViewModel model,
        [FromServices] AbissalSystemDbContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email
        };

        usuario.SenhaHash = PasswordHasher.Hash(model.Senha);

        try
        {
            await context.Usuario.AddAsync(usuario);
            await context.SaveChangesAsync();

            var token = tokenService.GenerateToken(usuario);

            return Ok(new ResultViewModel<dynamic>(new
            {   
                id = usuario.Id,
                name = usuario.Nome, 
                token
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("0199X - Este E-mail já está cadastrado"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("0199C - Falha interna no servidor"));
        }
    }

    [HttpPost("/auth/signin")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] AbissalSystemDbContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = await context
            .Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (usuario == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(usuario.SenhaHash, model.Senha))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(usuario);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("01200X - Falha interna no servidor"));
        }
    }
}