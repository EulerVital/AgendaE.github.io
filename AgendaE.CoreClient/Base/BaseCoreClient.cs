using AgendaE.CoreClient.Helper;
using Blazored.LocalStorage;
using Domain.Dto.Login;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Base
{
    public abstract class BaseCoreClient
    {
        protected readonly string CODIGO_VALID;
        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration Configuration;
        protected readonly LocalStorageService _localStorage;
        public abstract string EndPoint { get;}
        public int UsuarioId { get; set; }


        protected BaseCoreClient(HttpClient http, IConfiguration configuration, LocalStorageService localStorage)
        {
            string? codigo = Environment.GetEnvironmentVariable("Secrets_Codigo") ?? configuration["Secrets:Codigo"];

            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentNullException("Secrets não informado");

            _httpClient = http;
            Configuration = configuration;
            CODIGO_VALID = codigo;
            _localStorage = localStorage;
        }

        public virtual async Task<bool> ValidarToken()
        {
            var token = await _localStorage.GetItemAsync<LoginResultDto>("authToken");

            if(token == null)
                return false;

            return true;
        }

        protected virtual string UrlBase()
        {
            var url = Configuration["ApiUrl"];
            return url ?? string.Empty;
        }
    }
}
