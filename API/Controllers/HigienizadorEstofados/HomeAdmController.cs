using AgendaE.Core.Interface.HigienizadorEstofados;
using Asp.Versioning;
using Domain.Dto.App.HigienizadorEstofados;
using Domain.Entity.App.HigienizadorEstofados;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class HomeAdmController : ControllerBase
    {
        private readonly IHomeAdmCore _core;

        public HomeAdmController(IHomeAdmCore core)
        {
            _core = core;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int usuarioId, int count)
        {
            try
            {
                var list = await _core.GetLatestAsync(usuarioId, count);

                if (list == null)
                    return NotFound(new { Message = "Nenhuma solicitação enontrada" });

                List<SolicitacaoOrcamentoHEDto> listDto = list.Select(s => (SolicitacaoOrcamentoHEDto)s).ToList();

                return Ok(listDto);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

        [HttpGet("pendentes")]
        public async Task<IActionResult> Pendentes(int usuarioId)
        {
            try
            {
                int retorno = await _core.GetNewCount(usuarioId);
                return Ok(retorno);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

    }
}
