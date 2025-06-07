using Domain.Entity.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.Interface.HigienizadorEstofados
{
    public interface IOrcamentoHECore
    {
        Task<SolicitacaoOrcamentoHE?> Insert(SolicitacaoOrcamentoHE entity);
        Task<SolicitacaoOrcamentoHE?> GetById(int id);
        Task<SolicitacaoOrcamentoHE?> GetByCompletoId(int id);
    }
}
