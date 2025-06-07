using AgendaE.Core.Helper;
using AgendaE.Core.Interface;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Domain.Entity.Cadastro;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaE.Common.Extension;
using AgendaE.DataAccess;

namespace AgendaE.Core
{
    public class TipoCore : BaseService, ITipoAppCore
    {
        public TipoCore(ApplicationDbContext context, IDapperData dapperContext, IConfiguration configuration) : base(context, dapperContext, configuration)
        {
        }

        public async Task<TipoApp?> GetById(int id)
        {
            try
            {
                var dao = new TipoAppDao(_context, _dapperContext);
                return await dao.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TipoApp>> List(bool inativos = false)
        {
            try
            {
                var dao = new TipoAppDao(_context, _dapperContext);
                return await dao.List(inativos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
