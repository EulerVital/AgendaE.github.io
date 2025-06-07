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
    public class SolicitacaoOrcamentoHEDao : Repository<SolicitacaoOrcamentoHE>
    {
        public SolicitacaoOrcamentoHEDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<SolicitacaoOrcamentoHE?> GetById(int id)
        {
            return await _context.SolicitacaoOrcamentoHEs.FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task<SolicitacaoOrcamentoHE?> GetByIdAsNoTracking(int id)
        {
            return await _context.SolicitacaoOrcamentoHEs.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<SolicitacaoOrcamentoHE>> GetLatestAsync(int usuarioId, int count)
        {
            var list = await _context.SolicitacaoOrcamentoHEs
                .Include(i=>i.Endereco)
                .Where(w=>w.UsuarioId == usuarioId).OrderByDescending(r => r.DataSolicitacao).Take(count).ToListAsync();
            return list;
        }

        public override int RetornaId(SolicitacaoOrcamentoHE model)
        {
            return model.Id;
        }
    }
}
