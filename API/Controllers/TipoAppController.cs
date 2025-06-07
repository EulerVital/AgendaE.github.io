using AgendaE.Core.Interface;
using API.Helper;
using Asp.Versioning;
using Domain.Entity.Cadastro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using System.Net;

namespace API.Controllers
{
    [OpenApiRule]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TipoAppController : BaseController
    {
        private readonly ITipoAppCore _core;

        public TipoAppController(ITipoAppCore core, IConfiguration configuration) : base(configuration)
        {
            _core = core;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            try
            {
                return Ok(await _core.List());
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
                return Ok(await _core.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }
    }
}
