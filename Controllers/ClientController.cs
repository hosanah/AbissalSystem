using AbissalSystem.Data;
using AbissalSystem.Extensions;
using AbissalSystem.Models;
using AbissalSystem.ResultViewModel;
using AbissalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbissalSystem.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase{

        [HttpGet("v1/clients")]
        public async Task<IActionResult> GetAsyc([FromServices]AbissalSystemDbContext context)
        {
            try
            {
                var clients = await context.Clients.ToListAsync();
                return Ok(new ResultViewModel<List<Client>>(clients));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<List<Client>>("EXGCLI001 Não foi possível buscar todos os clients! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<List<Client>>("EXGCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpGet("v1/clients/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices]AbissalSystemDbContext context, [FromRoute] int id)
        {

            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);

                if (client == null)
                return NotFound(new ResultViewModel<Client>("EXGCLI005 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Client>("EXGCLI003 Não foi possível buscar esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Client>("EXGCLI004 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPost("v1/clients")]
        public async Task<IActionResult> CreateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorClientViewModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Client>(ModelState.GetErrors()));

            try
            {
                var client = new Client()
                {
                    Id = 0,
                    FullName = model.FullName,
                    Nickname = model.Nickname,
                    Birthday = model.Birthday,
                    CallPhone = model.CallPhone,
                    Email = model.Email
                };

                await context.Clients.AddAsync(client);
                await context.SaveChangesAsync();

                return Created($"v1/clients/{client.Id}", new ResultViewModel<Client>(client));

            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Client>("EXCCLI001 Não foi possível incluir esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Client>("EXCCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPut("v1/clients/{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorClientViewModel model,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Client>(ModelState.GetErrors()));
                
             try
            {
                 var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            
                if (client == null)
                return NotFound(new ResultViewModel<Client>("EXUCLI003 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                client.Birthday = model.Birthday;
                client.CallPhone = model.CallPhone;
                client.FullName = model.FullName;
                client.Nickname = model.Nickname;

                context.Clients.Update(client);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Client>("EXUCLI001 Não foi possível atualizar esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Client>("EXUCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpDelete("v1/clients/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices]AbissalSystemDbContext context,[FromRoute] int id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            
                if (client == null)
                return NotFound(new ResultViewModel<Client>("EXDCLI003 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                context.Clients.Remove(client);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Client>("EXDCLI001 Não foi possível incluir esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Client>("EXDCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }
    }
}