using AgendaE.Core.Interface.HigienizadorEstofados;
using Asp.Versioning;
using Domain.Dto.App.HigienizadorEstofados;
using Domain.Entity.App.HigienizadorEstofados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using System.Net;

namespace API.Controllers.HigienizadorEstofados
{
    [OpenApiRule]
    [ApiController]
    [Route("api/v{version:apiVersion}/[area]/[controller]")]
    [Area("HigienizadorEstofados")]
    [ApiVersion("1.0")]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoHECore _core;

        public OrcamentoController(IOrcamentoHECore core)
        {
            _core = core;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SolicitacaoOrcamentoHEInsertDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var obj = await _core.Insert(model);

                if (obj == null)
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });

                return CreatedAtAction(nameof(Index), new { id = obj.Id }, obj);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var obj = await _core.GetById(id);

                if (obj == null)
                    return NotFound(new { Message = "Solicitação de orçamento não encontrada" });

                SolicitacaoOrcamentoHEDto objOrcamento = obj;

                return Ok(objOrcamento);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

    }
}
