using AgendaE.Common.Base;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.Base
{
    public abstract class BaseDao
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IDapperData _dapperData;

        protected BaseDao(ApplicationDbContext context, IDapperData dapperData)
        {
            _dapperData = dapperData;
            _context = context;
        }

        protected virtual void AddFilterPagination(ref DynamicParameters dynamicParameters, BaseFilter filtro)
        {
            dynamicParameters.Add("PAGEINDEX", filtro.PageNumber, DbType.Int32);
            dynamicParameters.Add("PAGESIZE", filtro.PageSize, DbType.Int32);
            dynamicParameters.Add("ROWSCOUNT", 0, DbType.Int32, ParameterDirection.Output);
        }
    }
}
