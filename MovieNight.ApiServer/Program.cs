using Serilog;
using Azure.Identity;
using MovieNight.Data.DbContexts;
using MovieNight.Data;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Core.Handlers;
using Microsoft.Extensions.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddApiVersioning(setupAction =>
//{
//    setupAction.AssumeDefaultVersionWhenUnspecified = true;
//    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
//    setupAction.ReportApiVersions = true;
//});

// Add Azure App Configuration to the container.
var azAppConfigConnection = builder.Configuration["AppConfig"];

if (!string.IsNullOrEmpty(azAppConfigConnection))
{
    var credentials = new DefaultAzureCredential();
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(azAppConfigConnection)
        .ConfigureKeyVault(kv =>
        {
            kv.SetCredential(credentials);
        })
        .Select("MovieNight:*")
        .TrimKeyPrefix("MovieNight:");

    });
}
else if (Uri.TryCreate(builder.Configuration["Endpoints:AppConfig"], UriKind.Absolute, out var endpoint))
{
    // Use Azure Active Directory authentication.
    // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
    // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        var credentials = new ManagedIdentityCredential();
        options.Connect(endpoint, credentials)
        .ConfigureKeyVault(kv =>
        {
            kv.SetCredential(credentials);
        })
        .Select("MovieNight:*")
        .TrimKeyPrefix("MovieNight:");
    });
}

builder.Services.AddAzureAppConfiguration();
builder.Services.AddScoped<IMovieHandler, MovieHandler>();
builder.Services.PersistenceServiceRegistrations<MovieNightDbContext>(builder.Configuration);

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
