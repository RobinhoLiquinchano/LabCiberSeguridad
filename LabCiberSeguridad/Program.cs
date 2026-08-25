using LabCiberSeguridad.Services.EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Desactiva el monitoreo de cambios en archivos de configuración para evitar el límite de inotify en Linux
foreach (var source in builder.Configuration.Sources)
{
    if (source is Microsoft.Extensions.Configuration.FileConfigurationSource fileSource)
    {
        fileSource.ReloadOnChange = false;
    }
}

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