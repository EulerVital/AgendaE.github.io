using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using Blazored.LocalStorage;
using Domain.Dto.Usuario;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using AgendaE.CoreClient.Helper;

namespace UI.Components.Helper
{
    public abstract class BaseComponent : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected ISnackbar Snackbar { get; set; }

        [Inject]
        protected IDialogService DialogService { get; set; }

        [Inject]
        protected LocalStorageService LocalStorage { get; set; }

        [Inject]
        public IUsuarioCoreClient CoreUsuario { get; set; }

        [Inject]
        public IJSRuntime _js { get; set; }

        public virtual void Voltar(string url, bool force = false)
        {
            Navigation.NavigateTo(url, force);
        }

        public async Task<string> LoadImagem(IBrowserFile file)
        {
            if (file == null)
                return string.Empty;

            // Limita o tamanho para 5MB
            var maxAllowedSize = 5 * 1024 * 1024;
            if (file.Size > maxAllowedSize)
                return string.Empty;

            // Lê o arquivo como bytes
            var buffer = new byte[file.Size];
            await file.OpenReadStream(maxAllowedSize).ReadAsync(buffer);

            // Converte para base64 para exibição
            var imageBase64 = Convert.ToBase64String(buffer);
            return $"data:{file.ContentType};base64,{imageBase64}";
        }

        protected async Task<UsuarioDto?> CarregarUsuarioResponsavel(string codigo)
        {
            var objDto = await LocalStorage.GetItemAsync<UsuarioDto>(codigo);

            if (objDto == null)
            {
                objDto = await CoreUsuario.GetPorCodigo(codigo);

                if(objDto != null)
                    await LocalStorage.SetItemAsync(codigo, objDto);
            }

            return objDto;
        }

    }
}
