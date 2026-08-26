namespace Perfiles.Models
{
    public class PersonaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public IFormFile? Foto { get; set; } // Archivo binario enviado desde la vista o cliente API
    }

    public class PersonaUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public IFormFile? Foto { get; set; }
    }
}
