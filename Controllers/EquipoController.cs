using LIDOM.Context;
using LIDOM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIDOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : Controller
    {
        private readonly LIDOMContext _context;

        public EquipoController(LIDOMContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Equipo")]
        public async Task<ActionResult<Equipo>> PostEquipo(EquipoDTO equipoDTO)
        {
            var equipo = new Equipo
            {
                Nombre = equipoDTO.Nombre,
                Ciudad = equipoDTO.Ciudad,
                Estadio = equipoDTO.Estadio
            };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipo", new { id = equipo.IdEquipo }, equipo);
        }

        [HttpGet("Get-Equipos")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
            return await _context.Equipos.ToListAsync();
        }

        [HttpGet("Get-EquipoID")]
        public async Task<ActionResult<Equipo>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            return equipo;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Editar-Equipo/{id}")]
        public async Task<IActionResult> EditarEquipo(int id, EquipoDTO equipoDTO)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            

            if (equipo == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del equipo con los valores del DTO
            equipo.Nombre = equipoDTO.Nombre;
            equipo.Ciudad = equipoDTO.Ciudad;
            equipo.Estadio = equipoDTO.Estadio;

            // Guardar los cambios en la base de datos
            _context.Equipos.Update(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Eliminar-Equipo/{id}")]
        public async Task<IActionResult> EliminarEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
