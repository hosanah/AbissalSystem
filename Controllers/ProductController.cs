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
    public class ProductController : ControllerBase{

        [HttpGet("v1/produtos")]
        public async Task<IActionResult> GetAsyc([FromServices]AbissalSystemDbContext context)
        {
            try
            {
                var produtos = await context.Produtos.ToListAsync();
                return Ok(new ResultViewModel<List<Produto>>(produtos));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<List<Produto>>("EXGPRO001 Não foi possível buscar todos os produtos! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<List<Produto>>("EXGPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpGet("v1/produtos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices]AbissalSystemDbContext context, [FromRoute] int id)
        {

            try
            {
                var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                return NotFound(new ResultViewModel<Produto>("EXGPRO005 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXGPRO003 Não foi possível buscar esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXGPRO004 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPost("v1/produtos")]
        public async Task<IActionResult> CreateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorProdutoViewModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));

            try
            {
                var produto = new Produto()
                {
                    Id = 0,
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Preco = model.Preco
                };

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();

                return Created($"v1/products/{produto.Id}", new ResultViewModel<Produto>(produto));

            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXCPRO001 Não foi possível incluir esse producte! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXCPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPut("v1/produtos/{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorProdutoViewModel model,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));
                
             try
            {
                 var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            
                if (produto == null)
                return NotFound(new ResultViewModel<Produto>("EXUPRO003 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Preco = model.Preco;

                context.Produtos.Update(produto);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXUPRO001 Não foi possível atualizar esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXUPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpDelete("v1/produtos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices]AbissalSystemDbContext context,[FromRoute] int id)
        {
            try
            {
                var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            
                if (produto == null)
                return NotFound(new ResultViewModel<Produto>("EXDPRO003 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXDPRO001 Não foi possível incluir esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Produto>("EXDPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }
    }
}