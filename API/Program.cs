using AgendaE.Common.Settings;
using AgendaE.Core;
using AgendaE.Core.Helper;
using AgendaE.Core.HigienizadorEstofados;
using AgendaE.Core.Interface;
using AgendaE.Core.Interface.HigienizadorEstofados;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Adicionar serviços de versionamento de API
var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; // Exibe a versão da API nos cabeçalhos de resposta
    //options.ApiVersionReader = ApiVersionReader.Combine(
    //    new QueryStringApiVersionReader("api-version"), // via query string
    //    new HeaderApiVersionReader("x-api-version") // via cabeçalho
    //);
});


// Configurar o ApiExplorer para lidar com múltiplas versões no Swagger
apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Define o formato da versão como "vX.X"
    options.SubstituteApiVersionInUrl = true; // Substitui a versão na URL
});

if (jwtSettings != null)
    builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    if (jwtSettings == null) return;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
    };
});

builder.Services.AddAuthorization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebase-service-account.json")
});

// Add services to the container.
builder.Services.AddControllers()
                .AddDataAnnotationsLocalization();  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    IApiVersionDescriptionProvider provider = builder.Services.BuildServiceProvider()
                        .GetRequiredService<IApiVersionDescriptionProvider>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
    new string[] {}
    }});

    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerDoc(description.GroupName, new OpenApiInfo()
        {
            Title = $"API {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = "MTLog Transporte"
        });
    }
});

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDapperData, DapperData>();
builder.Services.AddScoped<IUsuarioCore, UsuarioCore>();
builder.Services.AddScoped<ITipoAppCore, TipoCore>();
builder.Services.AddScoped<IOrcamentoHECore, OrcamentoHECore>();
builder.Services.AddScoped<IHomeAdmCore, HomeAdmCore>();

builder.Services.AddScoped<ImageUploadService>();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 5 * 1024 * 1024; // 5MB
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder => builder
            .WithOrigins("http://localhost:63822", "https://localhost:44391")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
            
});

builder.Services.AddRouting();


var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowBlazorClient");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

app.Run();
