using Domain.Entity.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.Interface.HigienizadorEstofados
{
    public interface IHomeAdmCore
    {
        Task<List<SolicitacaoOrcamentoHE>> GetLatestAsync(int usuarioId, int count);
        Task<int> GetNewCount(int usuarioId);
    }
}
