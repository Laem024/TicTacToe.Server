using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using TicTacToe.Application.Mappings;
using TicTacToe.Infrastructure;
using TicTacToe.Persistence.Service;
using TicTacToe.Shared.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración de Serilog para generar logs
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Cambia a Warning para reducir más
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//ConnectionString
var dbProviderName  = builder.Configuration["DatabaseProvider:PrincipalDB"]
    ?? throw new InvalidOperationException("El nombre del proveedor de la base de datos no está definido.");

if (!Enum.TryParse<SupportedDbProvider>(dbProviderName , ignoreCase: true, out var dbProvider))
    throw new InvalidOperationException($"Proveedor de base de datos '{dbProviderName }' no soportado.");

var connectionString = builder.Configuration.GetConnectionString("SqlServerStr")
    ?? throw new InvalidOperationException($"La cadena de conexión 'SqlServerStr' no está configurada.");

builder.Services.AddPersistenceService(connectionString, dbProvider);

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddInfrastructure();

//Controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Configuracion de Swagger
    app.UseSwaggerUI(options => 
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1"));
}

app.UseHttpsRedirection();

//Controllers
app.MapControllers();

app.Run();