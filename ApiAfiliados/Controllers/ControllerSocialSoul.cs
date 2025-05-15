using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAfiliados.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SocialSoul : ControllerBase
    {
        private readonly IServicoSocialSoul _servicoSocialSoul;

        // Modificado para aceitar IServicoSocialSoul no construtor
        public SocialSoul(IServicoSocialSoul servicoSocialSoul)
        {
            _servicoSocialSoul = servicoSocialSoul ?? throw new ArgumentNullException(nameof(servicoSocialSoul));
        }


        [HttpGet]
        [Route("ObterOfertas")]

        public async Task<IActionResult> ObterOfertas([FromQuery] List<Produto> produtos)
        {
            return Execute(() => _servicoSocialSoul.ObterOfertas<Produto>(produtos));

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