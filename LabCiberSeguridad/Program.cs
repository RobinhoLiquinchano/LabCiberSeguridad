using LabCiberSeguridad.Services.EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Desactivamos la carga automática de archivos de configuración predeterminados que causan el error inotify
});

// Limpiamos y cargamos manualmente la configuración sin monitoreo de cambios
builder.Configuration.Sources.Clear();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();