using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using neuromasters.repositories;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações (exemplo simples)
var configuration = builder.Configuration;

    builder.Host.UseSerilog();
// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .CreateLogger();
try
{
    Log.Information("Starting Neuromasters API");


    // Configurar DbContext com PostgreSQL (ajuste a string de conexão no appsettings.json)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    // Adicionar controllers e configurar JSON para enums como string
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });

    // Configurar Swagger com título e versão
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Neuromasters API",
            Version = "v1",
            Description = "API para sistema de avaliação comportamental TEA"
        });
    });

    // Build app
    var app = builder.Build();

    // Middleware para Swagger (apenas em desenvolvimento)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neuromasters API v1");
            c.RoutePrefix = string.Empty; // Swagger na raiz (http://localhost:5000/)
        });
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}