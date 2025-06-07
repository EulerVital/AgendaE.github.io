using AgendaE.CoreClient.Contract.HigienizadorEstofados;
using AgendaE.CoreClient.Helper;
using AgendaE.CoreClient.HigienizadorEstofados;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UI.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Carrega as configurações do wwwroot
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true);

var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:5001"; // Pega do appsettings.json ou usa default

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped<IUsuarioCoreClient, UsuarioCoreClient>();

await builder.Build().RunAsync();
