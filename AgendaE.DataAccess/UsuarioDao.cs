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
    public class UsuarioDao : Repository<Usuario>
    {
        public UsuarioDao(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
        }

        public override async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Usuario?> GetLogin(string email, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(f => f.Email == email && f.Senha == senha);
        }

        public async Task<Usuario?> GetPorCodigo(string codigo)
        {
            var query = await (from u in _context.Usuarios
                               join c in _context.ConfiguracaoAppUsuarios
                               on u.Id equals c.UsuarioId
                               where c.Codigo == codigo
                               select u
                               ).FirstOrDefaultAsync();

            return query;
        }

        public override async Task<Usuario?> GetByIdAsNoTracking(int id)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public override int RetornaId(Usuario model)
        {
            return model.Id;
        }
    }
}
