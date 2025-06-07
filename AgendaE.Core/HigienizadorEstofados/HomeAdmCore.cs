using AgendaE.Core.Helper;
using AgendaE.Core.Interface.HigienizadorEstofados;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.HigienizadorEstofados;
using AgendaE.DataAccess.Interface;
using Domain.Entity.App.HigienizadorEstofados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.HigienizadorEstofados
{
    public class HomeAdmCore : BaseService, IHomeAdmCore
    {
        public HomeAdmCore(ApplicationDbContext context, IDapperData dapperContext, IConfiguration configuration) : base(context, dapperContext, configuration)
        {
        }

        public async Task<List<SolicitacaoOrcamentoHE>> GetLatestAsync(int usuarioId, int count)
        {
            var dao = new SolicitacaoOrcamentoHEDao(_context, _dapperContext);
            var list = await dao.GetLatestAsync(usuarioId, count);
            return list;
        }

        public async Task<int> GetNewCount(int usuarioId)
        {
            return await _context.SolicitacaoOrcamentoHEs.CountAsync(c => c.UsuarioId == usuarioId && c.Status == Common.Enum.App.HigienizadorEstofados.StatusOrcamentoHEEnum.Pendente);
        }
    }
}
