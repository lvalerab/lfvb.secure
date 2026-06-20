using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class EstadoTrabajoEntity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    
        public ICollection<LineaEjecucionTrabajoEntity> LineasEjecucion { get; set; } = new List<LineaEjecucionTrabajoEntity>();
    }
}
