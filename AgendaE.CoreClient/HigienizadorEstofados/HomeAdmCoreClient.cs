using AgendaE.CoreClient.Base;
using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using AgendaE.CoreClient.Helper;
using Domain.Dto.App.HigienizadorEstofados;
using Domain.Dto.Usuario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.HigienizadorEstofados
{
    public class HomeAdmCoreClient : BaseCoreClient, IHomeAdmCoreClient
    {
        public HomeAdmCoreClient(HttpClient http, IConfiguration configuration, LocalStorageService localStorage) : base(http, configuration, localStorage)
        {

        }

        public override string EndPoint { get => UrlBase() + "higienizadorestofados/homeadm"; }

        public async Task<List<SolicitacaoOrcamentoHEDto>?> GetLatestAsync(int count, int usuario)
        {
            try
            {
                if (_httpClient == null)
                    return null;

                if(!await ValidarToken())
                    return null;

                var custom = new CustomRequestHelper<object, List<SolicitacaoOrcamentoHEDto>?>(_httpClient, EndPoint);
                return await custom.Get(new { usuarioId = usuario, count });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetPendentes(int usuario)
        {
            try
            {
                if(_httpClient == null)
                    return 0;

                if(!await ValidarToken())
                    return 0;

                var custom = new CustomRequestHelper<object, int>(_httpClient, EndPoint + "/pendentes");
                return await custom.Get(new { usuarioId = usuario });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
