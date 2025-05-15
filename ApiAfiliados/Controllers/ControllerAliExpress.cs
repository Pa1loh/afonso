
using Microsoft.AspNetCore.Mvc;

namespace ApiAfiliados.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AliExpress : Controller
    {
        [HttpGet]
        public IActionResult RetornarAcessosDoUsuario()
        {

            return Ok("Usuário não autênticado.");
        }
    }
}
