using System;
using System.Collections.Generic;

namespace LIDOM.Models
{
    public partial class Partido
    {
        public int IdPartido { get; set; }
        public DateTime Fecha { get; set; }
        public int EquipoLocal { get; set; }
        public int EquipoVisitante { get; set; }
        public string Resultado { get; set; } = null!;
        public int CarrerasLocal { get; set; }
        public int CarrerasVisitante { get; set; }
        public int HitsLocal { get; set; }
        public int HitsVisitante { get; set; }
        public int ErroresLocal { get; set; }
        public int ErroresVisitante { get; set; }
        public int IdTemporada { get; set; }

        public virtual Equipo EquipoLocalNavigation { get; set; } = null!;
        public virtual Equipo EquipoVisitanteNavigation { get; set; } = null!;
        public virtual Temporada IdTemporadaNavigation { get; set; } = null!;
    }
}
