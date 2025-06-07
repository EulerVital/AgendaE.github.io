using AgendaE.DataAccess.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AgendaE.DataAccess.Base
{
    public class DapperData : IDapperData
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; set; } = "SqlServer";

        public DapperData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType command = CommandType.Text)
        {
            string conexao = _configuration.GetConnectionString(ConnectionString) ?? string.Empty;
            IEnumerable<T> retorno;

            using (var db = new SqlConnection(conexao))
            {
                CustomPropertyTypeMap<T>();
                await db.OpenAsync();
                retorno = await db.QueryAsync<T>(query, commandType: command);
            }

            return retorno;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text)
        {
            if (dynamicParameters == null)
                throw new ArgumentException("'dynamicParameters' não pode ser nulo");

            string conexao = _configuration.GetConnectionString(ConnectionString) ?? string.Empty;
            IEnumerable<T> retorno = null;

            if (!string.IsNullOrEmpty(conexao))
            {
                using (var db = new SqlConnection(conexao))
                {
                    CustomPropertyTypeMap<T>();
                    await db.OpenAsync();
                    retorno = await db.QueryAsync<T>(query, dynamicParameters, commandType: command);
                }
            }

            return retorno;
        }

        public async Task<T> QuerySingleAsync<T>(string query, CommandType command = CommandType.Text)
        {
            string conexao = _configuration.GetConnectionString(ConnectionString) ?? string.Empty;
            T retorno;

            using (var db = new SqlConnection(conexao))
            {
                CustomPropertyTypeMap<T>();
                await db.OpenAsync();
                retorno = await db.QuerySingleAsync<T>(query, commandType: command);
            }

            return retorno;
        }

        public async Task<T> QuerySingleAsync<T>(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text)
        {
            if (dynamicParameters == null)
                throw new ArgumentException("'dynamicParameters' não pode ser nulo");

            string conexao = _configuration.GetConnectionString(ConnectionString) ?? string.Empty;
            T retorno;

            using (var db = new SqlConnection(conexao))
            {
                CustomPropertyTypeMap<T>();
                await db.OpenAsync();
                retorno = await db.QuerySingleAsync<T>(query, dynamicParameters, commandType: command);
            }


            return retorno;
        }

        public async Task<int> Execute(string query, DynamicParameters dynamicParameters, CommandType command = CommandType.Text)
        {
            if (dynamicParameters == null)
                throw new ArgumentException("'dynamicParameters' não pode ser nulo");

            string conexao = _configuration.GetConnectionString(ConnectionString) ?? string.Empty;
            int retorno = 0;

            using (var db = new SqlConnection(conexao))
            {
                await db.OpenAsync();
                retorno = await db.ExecuteAsync(query, dynamicParameters, commandType: command);
            }


            return retorno;
        }

        private void CustomPropertyTypeMap<T>()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
            typeof(T), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
            prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }
    }
}
