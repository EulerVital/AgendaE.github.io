using Microsoft.AspNetCore.Mvc;

namespace API.Helper
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly string CODIGO_VALID;
        protected readonly IConfiguration Configuration;

        protected BaseController(IConfiguration configuration)
        {
            string? codigo = Environment.GetEnvironmentVariable("Secrets_Codigo") ?? configuration["Secrets:Codigo"];

            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentNullException("Secrets não informado");

            CODIGO_VALID = codigo;
            Configuration = configuration;
        }

        protected bool IsValidarCodigo(string codigo) => codigo == CODIGO_VALID;
        protected object ObterObjetoCodigoInvalid() => new { Message = "Código header inválido" };
    }
}
