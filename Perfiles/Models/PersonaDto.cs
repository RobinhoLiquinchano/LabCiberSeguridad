// Models/PersonaDto.cs
namespace Perfiles.Models
{
    public class PersonaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public List<string> Telefonos { get; set; } = new(); // Lista de números entrantes
    }

    public class PersonaUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public List<string> Telefonos { get; set; } = new(); // Lista actualizada
    }
}