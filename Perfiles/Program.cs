using Microsoft.EntityFrameworkCore;
using Perfiles.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// === CONFIGURACIÓN DE CONFIGURACIÓN Y ARCHIVOS JSON ===
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// === CONFIGURACIÓN DE BASE DE DATOS INTELIGENTE ===
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var envConnection = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreConnection")
                        ?? Environment.GetEnvironmentVariable("PostgreConnection");

    if (!string.IsNullOrWhiteSpace(envConnection))
    {
        options.UseNpgsql(envConnection);
        Console.WriteLine("Base de datos configurada mediante variable de entorno (Render)");
    }
    else
    {
        var localConnection = builder.Configuration.GetConnectionString("PostgreConnection");

        if (string.IsNullOrWhiteSpace(localConnection))
        {
            throw new InvalidOperationException("No se encontró ninguna cadena de conexión válida en appsettings.json ni en variables de entorno.");
        }

        options.UseNpgsql(localConnection);
        Console.WriteLine("Base de datos configurada mediante appsettings.json (Local)");
    }
});

// === SERVICIOS DE LA APLICACIÓN ===
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// === APLICAR MIGRACIONES AUTOMÁTICAMENTE ===
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones a la base de datos.");
    }
}

app.UseCors("AllowAll");

// === CONFIGURACIÓN DEL PIPELINE HTTP ===
app.MapOpenApi();
app.MapScalarApiReference();

// Redirigir la raíz "/" directamente a Scalar
app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

app.UseAuthorization();
app.MapControllers();

app.Run();