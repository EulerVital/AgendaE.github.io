using AgendaE.Common.Settings;
using AgendaE.Core.Helper;
using AgendaE.Core.Interface;
using API.Helper;
using Asp.Versioning;
using Domain.Dto.Login;
using Domain.Dto.Usuario;
using Domain.Entity.Cadastro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using System.Net;

namespace API.Controllers
{
    [OpenApiRule]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LoginController : BaseController
    {
        private readonly TokenService _tokenService;
        private readonly IUsuarioCore CoreUsuario;

        public LoginController(JwtSettings jwtSettings, IUsuarioCore usuarioCore, IConfiguration configuration):base(configuration) 
        {
            _tokenService = new TokenService(jwtSettings);
            CoreUsuario = usuarioCore;
        }

        [HttpPost()]
        public async Task<IActionResult> Index([FromBody] LoginRequestDto request)
        {
            var usuario = await CoreUsuario.GetLogin(request.Email, request.Password);

            if (usuario != null)
            {
                var token = _tokenService.GenerateToken(usuario.Id.ToString(), request.Email);
                return Ok(new LoginResultDto() { Token = token, Id = usuario.Id });
            }

            return Unauthorized();
        }
    }
}
