using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace AgendaE.Core.Helper
{
    public abstract class BaseServiceSimple
    {
        protected TransactionScope NewTransaction()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(10000)
                });
        }
    }

    public class BaseService : BaseServiceSimple
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IDapperData _dapperContext;
        protected readonly IConfiguration _configuration;
        protected string BaseDirectory { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        public BaseService(ApplicationDbContext context, IDapperData dapperContext, IConfiguration configuration)
        {
            _context = context;
            _dapperContext = dapperContext;
            _configuration = configuration;
        }

        private string RemoveAcentosCaracteresEspeciais(string input)
        {
            // Passo 1: Normalizar a string para decompor caracteres acentuados
            string normalizedString = input.Normalize(NormalizationForm.FormD);

            // Passo 2: Remover diacríticos (acentos) usando Regex
            string withoutAccents = Regex.Replace(normalizedString, @"\p{Mn}", "");

            // Passo 3: Remover caracteres especiais (qualquer coisa que não seja letra, número ou espaço)
            string output = Regex.Replace(withoutAccents, @"[^a-zA-Z0-9\s]", "");

            return output.ToLower();
        }
    }
}
