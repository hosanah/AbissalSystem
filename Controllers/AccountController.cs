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
    [HttpPost("/auth/cadastrar")]
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
            NomeUsuario = model.NomeUsuario,
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

    [HttpPost("/auth/autenticar")]
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
            .FirstOrDefaultAsync(x => x.NomeUsuario == model.Usuario);

        if (usuario == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(usuario.SenhaHash, model.Senha))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));
        
        try
        {
            var token = tokenService.GenerateToken(usuario);
            
            return Ok(new ResultViewModel<ResultLoginViewModel>(new ResultLoginViewModel(usuario, token)));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("01200X - Falha interna no servidor"));
        }
    }

    [HttpPost("/auth/reautenticar")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] LoginViewModel model,
        [FromServices] AbissalSystemDbContext context,
        [FromServices] TokenService tokenService)
    {

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        /*var accessToken = model.AccessToken;
        var refreshToken = model.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal.Value == null)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = await context
            .Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NomeUsuario == model.Usuario);

            if (usuario == null || usuario.RefreshToken != refreshToken || usuario.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        
        */
        var usuario = await context
            .Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NomeUsuario == model.Usuario);

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

    /*private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }*/
}