namespace Perfiles.Models
{
    public class PersonaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; } // Ahora recibe directamente el link de internet
    }

    public class PersonaUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; } // Ahora recibe directamente el link de internet
    }
}
