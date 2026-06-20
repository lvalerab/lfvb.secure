using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class ParametroTrabajoEntity
    {
        public int Id { get; set; } 
        public Guid IdTrabajo { get; set; }
        public Guid? IdLineaEjecucionTrabajo { get; set; } = null;  
        public string Nombre { get; set; } = string.Empty;  
        public string Valor { get; set; } = string.Empty;   

        public TrabajoEntity Trabajo { get; set; }  
        public LineaEjecucionTrabajoEntity? LineaEjecucionTrabajo { get; set; } = null;
    }
}
