using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class TrabajoEntity
    {
        public Guid Id { get; set; }        
        public Guid IdUsuarioProgramador { get; set; }
        public Guid? IdTrabajoPadre { get; set; } = null;
        public Guid? IdTarea { get; set; } = null;  
        public Guid? IdLineaEjecucionPadre { get; set; } = null;    
        public Guid IdTipoTrabajo { get; set; } 
        public DateTime FechaProgramacion { get; set; } 


        public UsuarioEntity UsuarioProgramador { get; set; }
        public TrabajoEntity? TrabajoPadre { get; set; } = null;
        public TareaEntity? Tarea { get; set; } = null; 
        public LineaEjecucionTrabajoEntity? LineaEjecucionCreador { get; set; } = null;
        public TipoTrabajoEntity TipoTrabajo { get; set; }

        public ICollection<LineaEjecucionTrabajoEntity> LineasEjecucion { get; set; } = new List<LineaEjecucionTrabajoEntity>();
        public ICollection<TrabajoEntity> TrabajosHijos { get; set; } = new List<TrabajoEntity>();
        public ICollection<ParametroTrabajoEntity> Parametros { get; set; } = new List<ParametroTrabajoEntity>();  
        public ICollection<LogTrabajoEntity> Logs { get; set; } = new List<LogTrabajoEntity>(); 
    }
}
