using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class EntradaCalendarioEntity
    {
        public Guid Id { get; set; }
        public Guid IdTipoEntradaCalendario { get; set; }
        public Guid IdUsuarioCreador { get; set; }  
        public String Titulo { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }        


        public TipoEntradaCalendarioEntity TipoEntrada { get; set; }
        public UsuarioEntity UsuarioCreador { get; set; }
        
        public ICollection<CalendarioUsuarioEntradasEntity> Calendarios { get; set; }
        public ICollection<ParticipantesEntradaCalendarioEntity> Participantes { get; set; }
        public ICollection<ElementoEntradaCalendarioEntity> Elementos { get; set; }
    }
}
