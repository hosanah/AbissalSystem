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

        var user = new User
        {
            Name = model.Name,
            Email = model.Email
        };

        user.PasswordHash = PasswordHasher.Hash(model.Password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var token = tokenService.GenerateToken(user);

            return Ok(new ResultViewModel<dynamic>(new
            {   
                id = user.Id,
                name = user.Name, 
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

        var user = await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("01200X - Falha interna no servidor"));
        }
    }
}