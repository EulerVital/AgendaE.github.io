using Blazored.LocalStorage;
using Domain.Dto.Login;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.CoreClient.Base
{
    public class CustomAuthStateProviderClient : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public CustomAuthStateProviderClient(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<LoginResultDto>("authToken");

            if (token == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var jwtToken = _tokenHandler.ReadJwtToken(token.Token);

            // Verifica se expirou
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                await Logout();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task Login(LoginResultDto token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
