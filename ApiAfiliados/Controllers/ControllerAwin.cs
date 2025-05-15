using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAfiliados.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Awin : Controller
    {
       private readonly IServicoAwin _servicoAwin;

        // Modificado para aceitar IServicoShopee no construtor
        public Awin(IServicoAwin servicoAwin)
        {
            _servicoAwin = servicoAwin ?? throw new ArgumentNullException(nameof(servicoAwin));
        }


        [HttpGet]
        [Route("ObterOfertas")]
        public async Task<IActionResult> ObterOfertas([FromQuery] List<Produto> produtos)
        {
            return Execute(() => _servicoAwin.ObterOfertas<Produto>(produtos));

        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message
                                  + "\r\n\r\n"
                                  + ex.InnerException?.Message
                                  + "\r\n\r\nErro , Erro na requisição.");
            }
        }

    }
}
