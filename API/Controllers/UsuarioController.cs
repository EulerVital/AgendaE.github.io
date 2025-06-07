using AgendaE.Core.Interface;
using API.Helper;
using Asp.Versioning;
using Domain.Dto.Usuario;
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
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioCore _core;

        public UsuarioController(IUsuarioCore core, IConfiguration configuration):base(configuration) 
        {
            _core = core;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromHeader] string codigo, UsuarioInsertDto model)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if(!IsValidarCodigo(codigo))
                    return BadRequest(ObterObjetoCodigoInvalid());

                var usuario = await _core.Insert(model);

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index([FromHeader] string codigo, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!IsValidarCodigo(codigo))
                    return BadRequest(ObterObjetoCodigoInvalid());

                var usuario = await _core.GetById(id);

                if (usuario == null)
                    return BadRequest(new {Message = "Usuário não encontrado !!"});

                UsuarioDto us = usuario;

                return Ok(us);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

        [HttpGet("Codigo")]
        public async Task<IActionResult> Index([FromHeader] string codigo, [FromQuery] string cod)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!IsValidarCodigo(codigo))
                    return BadRequest(ObterObjetoCodigoInvalid());

                var usuario = await _core.GetPorCodigo(cod);

                if (usuario == null)
                    return BadRequest(usuario);

                UsuarioDto us = usuario;
                us.Codigo = cod;

                return Ok(us);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Index([FromHeader] string codigo, UsuarioUpdateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!IsValidarCodigo(codigo))
                    return BadRequest(ObterObjetoCodigoInvalid());

                Usuario user = model;

                var id = await _core.Update(user);

                if (id <= 0)
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Usuário não encontrado !!" });

                return Ok(id);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" });
            }
        }
    }
}
