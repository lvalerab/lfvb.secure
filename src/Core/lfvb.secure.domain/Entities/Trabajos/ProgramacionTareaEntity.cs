using lfvb.secure.domain.Entities.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class ProgramacionTareaEntity
    {
        public Guid Id { get; set; }
        public Guid IdTarea { get; set; }
        public string DiasEjecucion { get; set; }="0000000"; // Representa los días de la semana en los que se ejecutará la tarea ( 0 = lunes, ..., 6 = domingo )   
        public TimeSpan HoraEjecucion { get; set; } // Representa la hora del día en la que se ejecutará la tarea
        public DateTime? EjecutarDia  { get; set; } // Representa el día específico en el que se ejecutará la tarea (opcional)
        public string CrearEntradaCalendario { get; set; }
        public Guid? IdEntradaCalendario { get; set; } // Representa el ID de la entrada de calendario asociada a la tarea programada (opcional)   

        public TareaEntity Tarea { get; set; } // Relación con la entidad Tarea
        public EntradaCalendarioEntity? EntradaCalendario { get; set; } // Relación con la entidad EntradaCalendario
    }
}
