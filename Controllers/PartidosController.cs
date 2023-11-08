using LIDOM.Context;
using LIDOM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LIDOM.Controllers
{
    [Route("api/[controller]")]
    public class PartidosController : Controller
    {
        private readonly LIDOMContext _context;

        public PartidosController(LIDOMContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Partido")]
        public async Task<ActionResult<Partido>> PostPartido(PartidoDTO partidoDTO)
        {
            var resultado = $"{partidoDTO.CarrerasLocal}-{partidoDTO.CarrerasVisitante}";

            var partido = new Partido
            {
                Fecha = partidoDTO.Fecha,
                EquipoLocal = partidoDTO.EquipoLocal,
                EquipoVisitante = partidoDTO.EquipoVisitante,
                Resultado = resultado, 
                CarrerasLocal = partidoDTO.CarrerasLocal,
                CarrerasVisitante = partidoDTO.CarrerasVisitante,
                HitsLocal = partidoDTO.HitsLocal,
                HitsVisitante = partidoDTO.HitsVisitante,
                ErroresLocal = partidoDTO.ErroresLocal,
                ErroresVisitante = partidoDTO.ErroresVisitante,
                IdTemporada = partidoDTO.IdTemporada
            };

            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartido", new { id = partido.IdPartido }, partido);
        }

        [HttpGet("Get-Partidos")]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidos()
        {
            return await _context.Partidos.ToListAsync();
        }

        [HttpGet("Get-Partido/{id}")]
        public async Task<ActionResult<Partido>> GetPartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);

            if (partido == null)
            {
                return NotFound();
            }

            return partido;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Editar-Partido/{id}")]
        public async Task<IActionResult> EditarPartido(int id, PartidoDTO partidoDTO)
        {
            var partido = await _context.Partidos.FindAsync(id);

            var resultado = $"{partidoDTO.CarrerasLocal}-{partidoDTO.CarrerasVisitante}";


            if (partido == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del partido con los valores del DTO
            partido.Fecha = partidoDTO.Fecha;
            partido.EquipoLocal = partidoDTO.EquipoLocal;
            partido.EquipoVisitante = partidoDTO.EquipoVisitante;
            partido.Resultado = resultado;
            partido.CarrerasLocal = partidoDTO.CarrerasLocal;
            partido.CarrerasVisitante = partidoDTO.CarrerasVisitante;
            partido.HitsLocal = partidoDTO.HitsLocal;
            partido.HitsVisitante = partidoDTO.HitsVisitante;
            partido.ErroresLocal = partidoDTO.ErroresLocal;
            partido.ErroresVisitante = partidoDTO.ErroresVisitante;
            partido.IdTemporada = partidoDTO.IdTemporada;

            // Guardar los cambios en la base de datos
            _context.Partidos.Update(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Eliminar-Partido/{id}")]
        public async Task<IActionResult> EliminarPartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);

            if (partido == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }






    }
}
