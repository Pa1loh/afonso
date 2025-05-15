using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAfiliados.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Shopee : ControllerBase
    {
        private readonly IServicoShopee _servicoShopee;

        // Modificado para aceitar IServicoShopee no construtor
        public Shopee(IServicoShopee servicoShopee)
        {
            _servicoShopee = servicoShopee ?? throw new ArgumentNullException(nameof(servicoShopee));
        }


        [HttpGet]
        [Route("ObterOfertas")]

        public async Task<IActionResult> ObterOfertas([FromQuery] List<Produto> produtos)
        {
            return Execute(() => _servicoShopee.ObterOfertas<Produto>(produtos));

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
