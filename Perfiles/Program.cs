using Microsoft.EntityFrameworkCore;
using Perfiles.Data;
using Scalar.AspNetCore;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

// Configuración de Supabase
var supabaseUrl = builder.Configuration["SupabaseSettings:Url"];
var supabaseKey = builder.Configuration["SupabaseSettings:Key"];

// Inicializamos el cliente directamente, sin las opciones adicionales
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey);

// Inyectamos el cliente como Singleton
builder.Services.AddSingleton(supabaseClient);

// Inyectamos el cliente como Singleton
builder.Services.AddSingleton(supabaseClient);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();