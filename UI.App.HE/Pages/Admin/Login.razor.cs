using AgendaE.CoreClient.Base;
using Domain.Dto.Login;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using UI.Components.Helper;
using static System.Net.WebRequestMethods;

namespace UI.App.HE.Pages.Admin
{
    public class LoginBase : BaseComponent
    {
        public string _email = "";
        public string _password = "";
        public string? _error;

        [Inject]
        public CustomAuthStateProviderClient AuthProvider { get; set; }

        public async Task LoginAsync()
        {
            _error = null;

            var response = await Http.PostAsJsonAsync("login", new LoginRequestDto { Email = _email, Password = _password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();

                if(result == null)
                {
                    _error = "Credenciais inválidas!";
                    return;
                }

                await AuthProvider.Login(result);

                Navigation.NavigateTo("/", true);
            }
            else
            {
                _error = "Credenciais inválidas!";
            }
        }
    }
}
