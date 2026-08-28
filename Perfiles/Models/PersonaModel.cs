// Models/PersonaModel.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perfiles.Models
{
    public class PersonaModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Column("foto_url")]
        public string? FotoUrl { get; set; }

        // Nuevo campo agregado
        [Column("detalle")]
        public string? Detalle { get; set; }

        public List<TelefonoModel> Telefonos { get; set; } = new();
    }

    public class TelefonoModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("numero")]
        [MaxLength(20)] // La longitud del campo teléfono ya está limitada a 20 aquí
        public string Numero { get; set; } = string.Empty;

        [Column("persona_id")]
        public int PersonaId { get; set; }
    }
}