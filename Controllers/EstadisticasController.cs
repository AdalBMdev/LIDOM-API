using LIDOM.Context;
using LIDOM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIDOM.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly LIDOMContext _context;

        public EstadisticasController(LIDOMContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ObtenerEstadisticasEquipos")]
        public IActionResult ObtenerEstadisticasEquipos()
        {
            var estadisticasEquipos = _context.Set<EstadisticasEquipo>()
                .FromSqlRaw("EXEC ObtenerEstadisticasEquipos")
                .ToList();

            return Ok(estadisticasEquipos);
        }
    }
}
