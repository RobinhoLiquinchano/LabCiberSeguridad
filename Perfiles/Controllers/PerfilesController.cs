using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perfiles.Data;
using Perfiles.Models;

namespace Perfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Supabase.Client _supabase;

        // Inyectamos el DbContext y el cliente de Supabase
        public PerfilesController(ApplicationDbContext context, Supabase.Client supabase)
        {
            _context = context;
            _supabase = supabase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaModel>>> GetPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaModel>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            return persona;
        }

        [HttpPost]
        public async Task<ActionResult<PersonaModel>> PostPersona([FromForm] PersonaCreateDto dto)
        {
            var persona = new PersonaModel
            {
                Nombre = dto.Nombre,
                FotoUrl = await GuardarImagenAsync(dto.Foto)
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, [FromForm] PersonaUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();

            persona.Nombre = dto.Nombre;

            if (dto.Foto != null)
            {
                // Si envían una nueva foto, reemplazamos la URL
                persona.FotoUrl = await GuardarImagenAsync(dto.Foto);
            }

            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método actualizado para subir a Supabase Storage
        private async Task<string?> GuardarImagenAsync(IFormFile? foto)
        {
            if (foto == null || foto.Length == 0) return null;

            // Generamos un nombre único para evitar colisiones
            var fileName = $"{Guid.NewGuid()}_{foto.FileName}";

            // Convertimos el IFormFile a un arreglo de bytes
            using var memoryStream = new MemoryStream();
            await foto.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            // Asegúrate de que el bucket "perfiles" exista en tu proyecto de Supabase
            var bucket = _supabase.Storage.From("perfiles");

            // Subimos el archivo
            await bucket.Upload(bytes, fileName, new Supabase.Storage.FileOptions { Upsert = false });

            // Retornamos la URL pública para guardarla en PostgreSQL
            return bucket.GetPublicUrl(fileName);
        }
    }
}