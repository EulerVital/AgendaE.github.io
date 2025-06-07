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
    public class ItemHigienizacaoHEDao : Repository<ItemHigienizacaoHE>
    {
        public ItemHigienizacaoHEDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<ItemHigienizacaoHE?> GetById(int id)
        {
            return await _context.ItemHigienizacaoHEs.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<ItemHigienizacaoHE>> ListPorSolicitacao(int solicitacaoId)
        {
            return await _context.ItemHigienizacaoHEs.Where(f => f.SolicitacaoId == solicitacaoId).ToListAsync();
        }

        public override async Task<ItemHigienizacaoHE?> GetByIdAsNoTracking(int id)
        {
            return await _context.ItemHigienizacaoHEs.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public override int RetornaId(ItemHigienizacaoHE model)
        {
            return model.Id;
        }
    }
}
