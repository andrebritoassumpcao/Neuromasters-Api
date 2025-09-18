using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using neuromasters.api.Configurations;
using neuromasters.api.Extensions;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Adapters;
using neuromasters.borders.Entities;
using neuromasters.repositories;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
using neuromasters.api.Models;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações (exemplo simples)
var configuration = builder.Configuration;
var appConfig = builder.Configuration.LoadConfiguration();


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

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AuthDbContext>(
        options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.AddIdentityCore<User>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    builder.Services
              .AddValidators()
              .AddUseCases()
              .AddRepositories(appConfig)
              .AddScoped<IActionResultConverter, ActionResultConverter>()
              .AddScoped<IUserAdapter, UserAdapter>()
              .AddScoped<IRoleAdapter, RoleAdapter>(); ;
    ;

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Neuromasters API",
            Version = "v1",
            Description = "API para sistema de avaliação comportamental TEA"
        });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neuromasters API v1");
            c.RoutePrefix = string.Empty; // Swagger na raiz (http://localhost:5000/)
        });
        app.MapSwagger().RequireAuthorization();
    }
    app.MapGet("/", () => "teste");

    //app.MapIdentityApi<User>();

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