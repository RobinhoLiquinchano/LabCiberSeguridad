// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Perfiles.Models;

namespace Perfiles.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PersonaModel> Personas { get; set; }
        public DbSet<TelefonoModel> Telefonos { get; set; } // Nuevo DbSet
    }
}