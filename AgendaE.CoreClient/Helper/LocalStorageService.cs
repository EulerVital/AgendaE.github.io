using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Helper
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorage;

        public LocalStorageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        // Gravar objeto (serializa automaticamente para JSON)
        public async Task SetItemAsync<T>(string key, T data)
        {
            await _localStorage.SetItemAsync(key, data);
        }

        // Ler objeto (desserializa automaticamente do JSON)
        public async Task<T?> GetItemAsync<T>(string key)
        {
            return await _localStorage.GetItemAsync<T>(key);
        }

        // Verificar se uma chave existe
        public async Task<bool> ContainsKeyAsync(string key)
        {
            return await _localStorage.ContainKeyAsync(key);
        }

        // Remover item por chave
        public async Task RemoveItemAsync(string key)
        {
            await _localStorage.RemoveItemAsync(key);
        }

        // Limpar todos os dados
        public async Task ClearAsync()
        {
            await _localStorage.ClearAsync();
        }

        // Obter todas as chaves
        public async Task<IEnumerable<string>> GetAllKeysAsync()
        {
            return await _localStorage.KeysAsync();
        }

        // Obter o comprimento (quantidade de itens armazenados)
        public async Task<int> GetLengthAsync()
        {
            return await _localStorage.LengthAsync();
        }
    }
}
