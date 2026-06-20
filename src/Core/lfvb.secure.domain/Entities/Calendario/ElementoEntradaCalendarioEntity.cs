using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class ElementoEntradaCalendarioEntity
    {
        
        public Guid IdEntradaCalendario { get; set; }
        public Guid IdElemento { get; set; }
        public String Datos { get; set; }
        public EntradaCalendarioEntity EntradaCalendario { get; set; }
    }
}
