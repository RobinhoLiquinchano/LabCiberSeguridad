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

        public PerfilesController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult<PersonaModel>> PostPersona([FromBody] PersonaCreateDto dto)
        {
            var persona = new PersonaModel
            {
                Nombre = dto.Nombre,
                FotoUrl = dto.FotoUrl // Asignamos el link directamente
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, [FromBody] PersonaUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();

            persona.Nombre = dto.Nombre;
            persona.FotoUrl = dto.FotoUrl; // Actualizamos el link directamente

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
    }
}