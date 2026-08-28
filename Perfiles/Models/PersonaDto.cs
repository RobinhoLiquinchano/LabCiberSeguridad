// Models/PersonaDto.cs
namespace Perfiles.Models
{
    public class PersonaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string? Detalle { get; set; } // Nuevo campo
        public List<string> Telefonos { get; set; } = new();
    }

    public class PersonaUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string? Detalle { get; set; } // Nuevo campo
        public List<string> Telefonos { get; set; } = new();
    }
}