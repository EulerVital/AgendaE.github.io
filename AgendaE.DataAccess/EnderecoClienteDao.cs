using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Domain.Entity.App;
using Domain.Entity.App.HigienizadorEstofados;
using Domain.Entity.Cadastro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess
{
    public class EnderecoClienteDao : Repository<EnderecoCliente>
    {
        public EnderecoClienteDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<EnderecoCliente?> GetById(int id)
        {
            return await _context.EnderecoClientes.FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task<EnderecoCliente?> GetByIdAsNoTracking(int id)
        {
            return await _context.EnderecoClientes.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public override int RetornaId(EnderecoCliente model)
        {
            return model.Id;
        }
    }
}
