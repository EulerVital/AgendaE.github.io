using Domain.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Contract.HigienizadorEstofados
{
    public interface IUsuarioCoreClient
    {
        Task<UsuarioDto?> GetPorCodigo(string cod);
    }
}
