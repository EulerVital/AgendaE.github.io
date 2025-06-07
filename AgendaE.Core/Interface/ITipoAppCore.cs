using Domain.Entity.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.Interface
{
    public interface ITipoAppCore
    {
        Task<TipoApp?> GetById(int id);
        Task<List<TipoApp>> List(bool inativos = false);
    }
}
