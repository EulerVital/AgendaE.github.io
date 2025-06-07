using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Domain.Entity.Cadastro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess
{
    public class TipoAppDao : Repository<TipoApp>
    {
        public TipoAppDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<TipoApp?> GetById(int id)
        {
            return await _context.TipoApps.FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task<TipoApp?> GetByIdAsNoTracking(int id)
        {
            return await _context.TipoApps.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<TipoApp>> List(bool inativos)
        {
            return await _context.TipoApps.Where(w=>w.Inativo == inativos).ToListAsync();
        }

        public override int RetornaId(TipoApp model)
        {
            return model.Id;
        }
    }
}
