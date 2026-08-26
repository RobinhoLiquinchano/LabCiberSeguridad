using Microsoft.EntityFrameworkCore; // 1. Asegúrate de tener este using
using Perfiles.Models;

namespace Perfiles.Data
{
    // 2. Agrega ": DbContext" aquí
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PersonaModel> Personas { get; set; }
    }
}