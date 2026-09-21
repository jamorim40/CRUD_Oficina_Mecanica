using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mecanica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [Authorize]
        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok("Teste de autorização bem-sucedido!");
        }
    }
}
