using AgendaE.CoreClient.Base;
using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using AgendaE.CoreClient.Helper;
using Domain.Dto.Usuario;
using Microsoft.Extensions.Configuration;

namespace AgendaE.CoreClient.HigienizadorEstofados
{
    public class UsuarioCoreClient : BaseCoreClient, IUsuarioCoreClient
    {
        public UsuarioCoreClient(HttpClient http, IConfiguration configuration, LocalStorageService localStorage) : base(http, configuration, localStorage)
        {

        }

        public override string EndPoint { get => UrlBase() + "Usuario"; }

        public async Task<UsuarioDto?> GetPorCodigo(string cod)
        {
            try
            {
                if (_httpClient == null)
                    return null;

                _httpClient.DefaultRequestHeaders.Add("codigo", CODIGO_VALID);

                var custom = new CustomRequestHelper<object, UsuarioDto?>(_httpClient, EndPoint + $"/codigo");
                return await custom.Get(new { cod });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
