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
    }
}
