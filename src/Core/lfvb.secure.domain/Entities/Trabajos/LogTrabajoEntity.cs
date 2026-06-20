using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class LogTrabajoEntity
    {
        public Int64 Id { get; set; }
        public Guid IdTrabajo { get; set; }
        public Guid? IdLineaEjecucionTrabajo { get; set; }
        public string Tipo { get; set; } = "I";
        public DateTime Fecha { get; set; } = DateTime.Now; 
        public string Mensaje { get; set; } = string.Empty;
        public string Datos { get; set; } = string.Empty;   


        public TrabajoEntity Trabajo { get; set; }
        public LineaEjecucionTrabajoEntity? LineaEjecucionTrabajo { get; set; } = null;
    }
}
