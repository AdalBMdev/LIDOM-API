using System;
using System.Collections.Generic;

namespace LIDOM.Models
{
    public partial class Temporada
    {
        public Temporada()
        {
            Partidos = new HashSet<Partido>();
        }

        public int IdTemporada { get; set; }
        public int Año { get; set; }

        public virtual ICollection<Partido> Partidos { get; set; }
    }
}
