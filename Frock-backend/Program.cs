using Microsoft.EntityFrameworkCore;
using Frock_backend.access_and_identity.Application.Interfaces;
using Frock_backend.access_and_identity.Application.Services;
using Frock_backend.access_and_identity.Domain.Repositories;
using Frock_backend.access_and_identity.Infrastructure.Persistence;
using Frock_backend.access_and_identity.Infrastructure.Repositories;

//SHARED
using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Repositories;
using Frock_backend.shared.Infrastructure.Interfaces.ASP.Configuration;
using Frock_backend.shared.Domain.Repositories;

//STOPS - Amir

using Frock_backend.stops.Application.Internal.CommandServices;
using Frock_backend.stops.Application.Internal.QueryServices;

using Frock_backend.stops.Domain.Repositories;
using Frock_backend.stops.Domain.Services;

using Frock_backend.stops.Infrastructure.Repositories;

//GEOGRAPHIC - Amir
using Frock_backend.stops.Application.Internal.CommandServices.Geographic;
using Frock_backend.stops.Application.Internal.QueryServices.Geographic;

using Frock_backend.stops.Domain.Repositories.Geographic;
using Frock_backend.stops.Domain.Services.Geographic;

using Frock_backend.stops.Infrastructure.Repositories.Geographic;

using Frock_backend.stops.Infrastructure.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CatchUP API",
        Version = "v1"
    });
});
// Database
builder.Services.AddDbContext<AccessIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// <summary>
/// Obtiene la cadena de conexión a la base de datos MySQL desde la configuración de la aplicación.
/// </summary>
/// <remarks>
/// El valor se extrae de la sección "ConnectionStrings" del archivo `appsettings.json`,
/// buscando la clave "DefaultConnection".
/// </remarks>
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });


// Configure Dependency Injection
// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// News Bounded Context Injection Configuration
// Access and Identity
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();

//Geographic
    builder.Services.AddScoped<IRegionRepository, RegionRepository>();
    builder.Services.AddScoped<IRegionCommandService, RegionCommandService>();
    builder.Services.AddScoped<IRegionQueryService, RegionQueryService>();
        /**/
    builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
    builder.Services.AddScoped<IProvinceCommandService, ProvinceCommandService>();
    builder.Services.AddScoped<IProvinceQueryService, ProvinceQueryService>();
        /**/
    builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
    builder.Services.AddScoped<IDistrictCommandService, DistrictCommandService>();
    builder.Services.AddScoped<IDistrictQueryService, DistrictQueryService>();
        /**/
    builder.Services.AddScoped<ILocalityRepository, LocalityRepository>();
    builder.Services.AddScoped<ILocalityCommandService, LocalityCommandService>();
    builder.Services.AddScoped<ILocalityQueryService, LocalityQueryService>();

//Stops
    builder.Services.AddScoped<IStopRepository, StopRepository>();
    builder.Services.AddScoped<IStopCommandService, StopCommandService>();
    builder.Services.AddScoped<IStopQueryService, StopQueryService>();

//Seeding Service Geographic Data
// Datos iniciales fijos de datos geográficos
builder.Services.AddScoped<GeographicDataSeeder>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    // Seed initial geographic data
    try
    {
        var seeder = services.GetRequiredService<GeographicDataSeeder>();
        await seeder.SeedDataAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error durante la carga de datos iniciales.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();