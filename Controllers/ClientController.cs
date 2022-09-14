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

        [HttpGet("v1/clientes")]
        public async Task<IActionResult> GetAsyc([FromServices]AbissalSystemDbContext context)
        {
            try
            {
                var clientes = await context.Clientes.ToListAsync();
                return Ok(new ResultViewModel<List<Cliente>>(clientes));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<List<Cliente>>("EXGCLI001 Não foi possível buscar todos os clientes! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<List<Cliente>>("EXGCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpGet("v1/clientes/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices]AbissalSystemDbContext context, [FromRoute] int id)
        {

            try
            {
                var clientes = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

                if (clientes == null)
                return NotFound(new ResultViewModel<Cliente>("EXGCLI005 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                return Ok(new ResultViewModel<Cliente>(clientes));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXGCLI003 Não foi possível buscar esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXGCLI004 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPost("v1/clientes")]
        public async Task<IActionResult> CreateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorClienteViewModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErrors()));

            try
            {
                var cliente = new Cliente()
                {
                    Id = 0,
                    NomeCompleto = model.NomeCompleto,
                    Apelido = model.Apelido,
                    DataAniversario = model.DataAniversario,
                    NumeroCelular = model.NumeroCelular,
                    Email = model.Email
                };

                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();

                return Created($"v1/clients/{cliente.Id}", new ResultViewModel<Cliente>(cliente));

            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXCCLI001 Não foi possível incluir esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXCCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPut("v1/clientes/{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorClienteViewModel model,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErrors()));
                
             try
            {
                 var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            
                if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("EXUCLI003 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                cliente.DataAniversario = model.DataAniversario;
                cliente.NumeroCelular = model.NumeroCelular;
                cliente.NomeCompleto = model.NomeCompleto;
                cliente.Apelido = model.Apelido;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXUCLI001 Não foi possível atualizar esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXUCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpDelete("v1/clientes/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices]AbissalSystemDbContext context,[FromRoute] int id)
        {
            try
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            
                if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("EXDCLI003 Não foi encontrado nenhum cliente com o código fornecido! Favor contate o suporte"));

                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXDCLI001 Não foi possível incluir esse cliente! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Cliente>("EXDCLI002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }
    }
}