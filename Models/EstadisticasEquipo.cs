using System;
using System.Collections.Generic;

namespace LIDOM.Models
{
    public class EstadisticasEquipo
    {
        public int EquipoID { get; set; }
        public string NombreEquipo { get; set; }
        public int JuegosGanados { get; set; }
        public int JuegosPerdidos { get; set; }
        public int JuegosEmpatados { get; set; }
        public int HitsTotales { get; set; }
        public int ErroresTotales { get; set; }
        public int CarrerasTotales { get; set; }
        public decimal PorcentajeVictorias { get; set; }
    }



}
