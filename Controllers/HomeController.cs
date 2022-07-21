using Microsoft.AspNetCore.Mvc;

namespace AbissalSystem.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get(){
            return Ok();
        }
    }
}