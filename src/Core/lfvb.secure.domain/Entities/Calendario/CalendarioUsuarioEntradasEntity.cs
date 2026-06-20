using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class CalendarioUsuarioEntradasEntity
    {        
        public Guid IdCalendarioUsuario { get; set; }
        public Guid IdEntradaCalendario { get; set; }


        public CalendarioUsuarioEntity CalendarioUsuario { get; set; }
        public EntradaCalendarioEntity EntradaCalendario { get; set; }
    }
}
