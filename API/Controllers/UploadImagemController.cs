using AgendaE.Core.Helper;
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
    public class UploadImagemController : BaseController
    {
        private readonly ImageUploadService _uploadService;

        public UploadImagemController(ImageUploadService uploadService, IConfiguration configuration) :base(configuration) 
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] string imagem)
        {
            try
            {                
                var file = Base64Converter.Base64ToIFormFile(imagem, "imagem");

                if (file == null || file.Length == 0)
                    return BadRequest(new { Message = "Nenhum arquivo enviado" });

                if (file.Length > 5 * 1024 * 1024) // 5MB
                    return BadRequest(new { Message = "Tamanho do arquivo excede o limite de 5MB" });

                if (!file.ContentType.StartsWith("image/"))
                    return BadRequest(new { Message = "Apenas arquivos de imagem são permitidos" });

                var imageUrl = await _uploadService.UploadImageAsync(file);
                return Ok(new { Url = imageUrl });
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" }); 
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] string urlImagem)
        {
            try
            {                
                bool retorno = await _uploadService.DeleteImageAsync(urlImagem);
                return Ok(retorno);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), new { Message = "Ocorreu um erro interno" }); 
            }
        }
    }
}
