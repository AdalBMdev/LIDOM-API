using LIDOM.Context;
using LIDOM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LIDOM.Controllers
{
    [Route("api/[controller]")]
    public class TemporadasController : ControllerBase
    {
        private readonly LIDOMContext _context;

        public TemporadasController(LIDOMContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Temporada")]
        public async Task<ActionResult<Temporada>> PostTemporada(TemporadaDTO temporadaDTO)
        {
            var temporada = new Temporada
            {
                Año = temporadaDTO.Año
            };

            _context.Temporadas.Add(temporada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemporada", new { id = temporada.IdTemporada }, temporada);
        }

        [HttpGet("Get-temporada")]
        public async Task<ActionResult<IEnumerable<Temporada>>> GetTemporadas()
        {
            return await _context.Temporadas.ToListAsync();
        }

        [HttpGet("Get-temporadaID")]
        public async Task<ActionResult<Temporada>> GetTemporada(int id)
        {
            var temporada = await _context.Temporadas.FindAsync(id);

            if (temporada == null)
            {
                return NotFound();
            }

            return temporada;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Editar-Temporada/{id}")]
        public async Task<IActionResult> EditarTemporada(int id, TemporadaDTO temporadaDTO)
        {
            var temporada = await _context.Temporadas.FindAsync(id);

            if (temporada == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades de la temporada con los valores del DTO
            temporada.Año = temporadaDTO.Año;


            // Guardar los cambios en la base de datos
            _context.Temporadas.Update(temporada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Eliminar-Temporada/{id}")]
        public async Task<IActionResult> EliminarTemporada(int id)
        {
            // Buscar la temporada por ID
            var temporada = await _context.Temporadas
                .Include(t => t.Partidos) // Cargar los partidos relacionados
                .FirstOrDefaultAsync(t => t.IdTemporada == id);

            if (temporada == null)
            {
                return NotFound();
            }

            if (temporada.Partidos.Any())
            {
                // Si la temporada tiene partidos relacionados, puedes elegir cómo manejarlos
                // En este ejemplo, los eliminaremos
                _context.Partidos.RemoveRange(temporada.Partidos);
            }

            // A continuación, elimina la temporada
            _context.Temporadas.Remove(temporada);

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Maneja cualquier excepción relacionada con la base de datos, como las restricciones de clave externa
                // Puedes registrar el error o devolver un mensaje de error personalizado
                return BadRequest("No se pudo eliminar la temporada debido a registros relacionados.");
            }

            return NoContent();
        }


    }
}
