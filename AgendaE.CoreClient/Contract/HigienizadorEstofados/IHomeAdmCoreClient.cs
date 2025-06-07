using Domain.Dto.App.HigienizadorEstofados;
using Domain.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Contract.HigienizadorEstofados
{
    public interface IHomeAdmCoreClient
    {
        Task<List<SolicitacaoOrcamentoHEDto>?> GetLatestAsync(int count, int usuario);
        Task<int> GetPendentes(int usuario);
    }
}
