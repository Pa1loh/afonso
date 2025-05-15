using Dominio;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico;

namespace ApiAfiliados.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContaTelegram : ControllerBase
    {
        private readonly IServicoContaTelegram _servicoContaTelegram;

        public ContaTelegram(IServicoContaTelegram servicoTelegram)
        {
            _servicoContaTelegram = servicoTelegram ?? throw new ArgumentNullException(nameof(servicoTelegram));
        }


        [HttpPost]
        [Route("Inserir")]
        public async Task<IActionResult> Inserir([FromBody] ModeloEntradaContaTelegram model)
        {
            if (model == null)
                return NotFound();

            return Execute(() => _servicoContaTelegram.Create<ModeloEntradaContaTelegram, ModeloSaidaContaTelegram, ValidadorContaTelegram>(model));
        }

        [HttpGet]
        [Route("ListarTodos")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarTodos()
        {
            return Execute(() => _servicoContaTelegram.GetAll<ModeloSaidaContaTelegram>());
        }


        [HttpGet]
        [Route("FiltrarPorId/{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _servicoContaTelegram.GetById<ModeloSaidaContaTelegram>(id));
        }


        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ModeloAtualizacaoContaTelegram model)
        {
            if (model is null)
                return BadRequest();

            return Execute(() =>
                _servicoContaTelegram.Update<ModeloAtualizacaoContaTelegram, ModeloSaidaContaTelegram, ValidadorContaTelegram>(model));
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                _servicoContaTelegram.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro ao executar a exclusão, entre em contato com o setor de TI.");
            }
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
