using System;
using System.Collections.Generic;

namespace LIDOM.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            PartidoEquipoLocalNavigations = new HashSet<Partido>();
            PartidoEquipoVisitanteNavigations = new HashSet<Partido>();
        }

        public int IdEquipo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Estadio { get; set; } = null!;

        public virtual ICollection<Partido> PartidoEquipoLocalNavigations { get; set; }
        public virtual ICollection<Partido> PartidoEquipoVisitanteNavigations { get; set; }
    }
}
