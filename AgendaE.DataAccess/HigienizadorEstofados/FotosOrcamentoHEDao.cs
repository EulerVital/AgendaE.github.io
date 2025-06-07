using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Domain.Entity.App.HigienizadorEstofados;
using Domain.Entity.Cadastro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.HigienizadorEstofados
{
    public class FotosOrcamentoHEDao : Repository<FotosOrcamentoHE>
    {
        public FotosOrcamentoHEDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<FotosOrcamentoHE?> GetById(int id)
        {
            return await _context.FotosOrcamentoHEs.FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task<FotosOrcamentoHE?> GetByIdAsNoTracking(int id)
        {
            return await _context.FotosOrcamentoHEs.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<FotosOrcamentoHE>> ListPorSolicitacao(int solicitacaoId)
        {
            return await _context.FotosOrcamentoHEs.Where(f => f.SolicitacaoId == solicitacaoId).ToListAsync();
        }

        public override int RetornaId(FotosOrcamentoHE model)
        {
            return model.Id;
        }
    }
}
