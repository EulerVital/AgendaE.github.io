using AgendaE.CoreClient.Helper;
using Blazored.LocalStorage;
using Domain.Dto.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Base
{
    public class CustomRequestHelper<TModelRequest, TModelResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly string? _url;

        private string endPointRequest;
        public string EndPointRequest { get { return _url + endPointRequest; } set { endPointRequest = value; } }

        public string ReturnNotSucesso { get; set; }

        public CustomRequestHelper(HttpClient httpClient, string url)
        {
            _httpClient = httpClient;
            _url = url;
        }

        public async Task<TModelResponse?> Get(TModelRequest filtro)
        {

            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            if (filtro == null)
                throw new ArgumentNullException(nameof(filtro));

            try
            {
                string _urlNew = $"{EndPointRequest}?{CreateQueryString(filtro)}";

                var response = await _httpClient.GetAsync(_urlNew);

                string retorno = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModelResponse>(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TModelResponse?> Get()
        {

            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var response = await _httpClient.GetAsync(EndPointRequest);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : string.Empty;
            return JsonConvert.DeserializeObject<TModelResponse>(retorno);
        }

        public async Task<string> GetString()
        {

            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var response = await _httpClient.GetAsync(EndPointRequest);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : string.Empty;
            return retorno;
        }

        public async Task<TModelResponse?> Post(TModelRequest requestBody)
        {
            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var teste = await content.ReadAsStringAsync();
            var response = await _httpClient.PostAsync(EndPointRequest, content);

            if (!string.IsNullOrEmpty(teste) && !(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.BadRequest))
                ReturnNotSucesso = teste;

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.BadRequest ? await response.Content.ReadAsStringAsync() : string.Empty;

            try
            {
                return JsonConvert.DeserializeObject<TModelResponse>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(retorno, ex);
            }
        }

        public async Task<TModelResponse?> PostPersonalizado(TModelRequest requestBody)
        {
            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var teste = await content.ReadAsStringAsync();
            var response = await _httpClient.PostAsync(EndPointRequest, content);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.BadRequest || 
                response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity || response.StatusCode == System.Net.HttpStatusCode.Conflict ? await response.Content.ReadAsStringAsync() : string.Empty;

            try
            {
                if(response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    ReturnNotSucesso = retorno;
                    return JsonConvert.DeserializeObject<TModelResponse>(string.Empty);
                }

                return JsonConvert.DeserializeObject<TModelResponse>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(retorno, ex);
            }
        }

        public async Task<TModelResponse?> Put(TModelRequest requestBody)
        {
            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(EndPointRequest, content);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : string.Empty;
            return JsonConvert.DeserializeObject<TModelResponse>(retorno);
        }

        public async Task<TModelResponse?> Patch(TModelRequest requestBody)
        {
            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, EndPointRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : string.Empty;
            return JsonConvert.DeserializeObject<TModelResponse>(retorno);
        }

        public async Task<TModelResponse?> Delete()
        {
            if (string.IsNullOrEmpty(EndPointRequest))
                throw new ArgumentNullException(nameof(EndPointRequest), "Adicione EndPointAPI no appsettings.[nome_do_ambiente].json");

            var response = await _httpClient.DeleteAsync(EndPointRequest);

            string retorno = response.StatusCode == System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : string.Empty;
            return JsonConvert.DeserializeObject<TModelResponse>(retorno);
        }

        private string CreateQueryString(object queryParams)
        {
            var properties = queryParams.GetType().GetProperties();

            var queryString = string.Join("&", properties
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(queryParams)?.ToString() ?? "")}"));

            return queryString;
        }
    }
}
