using lfvb.secure.domain.Entities.Hydra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class LineaEjecucionTrabajoEntity
    {
        public Guid Id { get; set; }  
        public Guid IdTrabajo { get; set; }  
        public Guid? IdHydra { get; set; }  
        public DateTime FechaLineaEjecucion { get; set; }  
        public string CodigoEstadoTrabajo { get; set; }
        public bool EnEsperaDeTrabajos { get; set; }
        public bool EnEsperaDeInteractivo { get; set; } 


        public TrabajoEntity Trabajo { get; set; }
        public HydraEntity? Hydra { get; set; }
        public EstadoTrabajoEntity EstadoTrabajo { get; set; }  

        public ICollection<TrabajoEntity> TrabajosCreados= new List<TrabajoEntity>();   
        public ICollection<ParametroTrabajoEntity> Parametros= new List<ParametroTrabajoEntity>();
    }
}
