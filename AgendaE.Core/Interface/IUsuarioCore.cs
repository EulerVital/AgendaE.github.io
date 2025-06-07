using Domain.Entity.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.Interface
{
    public interface IUsuarioCore
    {
        Task<int> Insert(Usuario model);
        Task<int> Update(Usuario model);
        Task<int> RedefinirSenha(int id, string senha);
        Task<Usuario?> GetById(int id);
        Task<Usuario?> GetPorCodigo(string codigo);
        Task<Usuario?> GetLogin(string email, string senha);
        Task<Usuario?> GetByIdAsNoTracking(int id);
    }
}
