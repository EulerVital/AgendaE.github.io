using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.Interface
{
    public interface IDapperData
    {
        public Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType command = CommandType.Text);
        public Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text);

        public Task<T> QuerySingleAsync<T>(string query, CommandType command = CommandType.Text);
        public Task<T> QuerySingleAsync<T>(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text);
        public Task<int> Execute(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text);

    }
}
