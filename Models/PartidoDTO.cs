using System;
using System.Collections.Generic;

namespace LIDOM.Models
{
    public class PartidoDTO
    {
        public DateTime Fecha { get; set; }
        public int EquipoLocal { get; set; }
        public int EquipoVisitante { get; set; }
        public string Resultado { get; set; }
        public int CarrerasLocal { get; set; }
        public int CarrerasVisitante { get; set; }
        public int HitsLocal { get; set; }
        public int HitsVisitante { get; set; }
        public int ErroresLocal { get; set; }
        public int ErroresVisitante { get; set; }
        public int IdTemporada { get; set; }
    }

}
