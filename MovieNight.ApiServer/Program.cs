using Azure.Identity;
using Microsoft.OpenApi.Models;
using MovieNight.Core.Handlers;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Data;
using MovieNight.Data.DbContexts;
using Serilog;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;

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

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Movie Night",
        Version = "v1",
        Description = "Api for our MovieNight application. Movies are from RapidApi and mainly from IMDB sources.",
        Contact = new OpenApiContact
        {
            Name = "Adam Moricz",
            Email = "adam.moricz@gmail.com"
        }
    });
    //swagger generator will look for xml file in the same directory as the assembly
    setup.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase  = true;
    

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setup.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        builder => 
            builder.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

//builder.Services.AddApiVersioning(setupAction =>
//{
//    setupAction.AssumeDefaultVersionWhenUnspecified = true;
//    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
//    setupAction.ReportApiVersions = true;
//});

var endpoint = builder.Configuration["Endpoints:AppConfig"];
var credentials = new DefaultAzureCredential();
builder.Configuration.AddAzureAppConfiguration(opt =>
{
    opt.Connect(new Uri(endpoint!), credentials)
        .ConfigureKeyVault(kv =>
        {
            kv.SetCredential(credentials);
        })
        .Select("MovieNight:*")
        .TrimKeyPrefix("MovieNight:");
});

builder.Services.AddAzureAppConfiguration();
builder.Services.AddScoped<IMovieHandler, MovieHandler>();
builder.Services.PersistenceServiceRegistrations<MovieNightDbContext>(builder.Configuration);

var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI();

app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
