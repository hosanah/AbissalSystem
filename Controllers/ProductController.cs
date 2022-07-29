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

        [HttpGet("v1/products")]
        public async Task<IActionResult> GetAsyc([FromServices]AbissalSystemDbContext context)
        {
            try
            {
                var products = await context.Products.ToListAsync();
                return Ok(new ResultViewModel<List<Product>>(products));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<List<Product>>("EXGPRO001 Não foi possível buscar todos os produtos! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<List<Product>>("EXGPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices]AbissalSystemDbContext context, [FromRoute] int id)
        {

            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                return NotFound(new ResultViewModel<Product>("EXGPRO005 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Product>("EXGPRO003 Não foi possível buscar esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Product>("EXGPRO004 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> CreateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorProductViewModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErros()));

            try
            {
                var product = new Product()
                {
                    Id = 0,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/products/{product.Id}", new ResultViewModel<Product>(product));

            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Product>("EXCPRO001 Não foi possível incluir esse producte! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Product>("EXCPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromServices]AbissalSystemDbContext context, [FromBody] EditorProductViewModel model,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErros()));
                
             try
            {
                 var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            
                if (product == null)
                return NotFound(new ResultViewModel<Product>("EXUPRO003 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;

                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Product>("EXUPRO001 Não foi possível atualizar esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Product>("EXUPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices]AbissalSystemDbContext context,[FromRoute] int id)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            
                if (product == null)
                return NotFound(new ResultViewModel<Product>("EXDPRO003 Não foi encontrado nenhum produto com o código fornecido! Favor contate o suporte"));

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex){
                return StatusCode(500, new ResultViewModel<Product>("EXDPRO001 Não foi possível incluir esse produto! Favor contate o suporte"));
            }
            catch (Exception ex){
                return StatusCode(500, new ResultViewModel<Product>("EXDPRO002 Erro interno do servidor! Favor contate o suporte"));
            }
            
        }
    }
}