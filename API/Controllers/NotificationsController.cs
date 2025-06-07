using API.Helper;
using Asp.Versioning;
using Domain.Dto;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;

namespace API.Controllers
{
    [OpenApiRule]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class NotificationsController : BaseController
    {
        public NotificationsController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] NotificationRequestDto req)
        {
            var message = new Message
            {
                Token = req.Token,
                Data = new Dictionary<string, string>
                {
                    { "title", req.Title },
                    { "body", req.Body },
                    { "icon", "icon-512.png" }, // Use um caminho relativo ou URL válido
                    { "url", "http://localhost:63821/lista-solicitacoes-orcamento" } // Link para redirecionar ao clicar
                }
            };
            var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(result);
        }
    }
}
