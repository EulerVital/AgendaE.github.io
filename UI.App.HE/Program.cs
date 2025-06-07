using AgendaE.CoreClient.Base;
using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using AgendaE.CoreClient.HigienizadorEstofados;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http;
using UI.App.HE;
using AgendaE.CoreClient.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Carrega as configurações do wwwroot
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true);

var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:5001"; // Pega do appsettings.json ou usa default

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProviderClient>();
builder.Services.AddScoped<CustomAuthStateProviderClient>();
builder.Services.AddScoped<AuthHeaderHandler>();

builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
.AddHttpMessageHandler<AuthHeaderHandler>();

// Registra o HttpClient padrão com o nome "API"
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));
builder.Services.AddScoped<LocalStorageService>();

#region Services Clients

builder.Services.AddScoped<IUsuarioCoreClient, UsuarioCoreClient>();
builder.Services.AddScoped<IHomeAdmCoreClient, HomeAdmCoreClient>();

#endregion

await builder.Build().RunAsync();
