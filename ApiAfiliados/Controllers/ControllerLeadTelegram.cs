using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico;

namespace ApiAfiliados.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LeadTelegram : ControllerBase
    {
        private readonly IServicoLeadTelegram _servicoLeadTelegram;

        public LeadTelegram(IServicoLeadTelegram leadTelegram)
        {
            _servicoLeadTelegram = leadTelegram ?? throw new ArgumentNullException(nameof(leadTelegram));
        }


        [HttpPost]
        [Route("Inserir")]
        public async Task<IActionResult> Inserir([FromBody] ModeloEntradaLeadTelegram model)
        {
            if (model == null)
                return NotFound();

            return Execute(() => _servicoLeadTelegram.Create<ModeloEntradaLeadTelegram, ModeloSaidaLeadTelegram, ValidadorLeadTelegram>(model));
        }

        [HttpGet]
        [Route("ListarTodos")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarTodos()
        {
            return Execute(() => _servicoLeadTelegram.GetAll<ModeloSaidaLeadTelegram>());
        }


        [HttpGet]
        [Route("FiltrarPorId/{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _servicoLeadTelegram.GetById<ModeloSaidaLeadTelegram>(id));
        }


        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ModeloAtualizacaoLeadTelegram model)
        {
            if (model is null)
                return BadRequest();

            return Execute(() =>
                _servicoLeadTelegram.Update<ModeloAtualizacaoLeadTelegram, ModeloSaidaLeadTelegram, ValidadorLeadTelegram>(model));
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                _servicoLeadTelegram.Delete(id);
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
