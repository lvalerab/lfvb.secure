using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class TipoViaEntity
    {
        public string Codigo { get; set; }  
        public string Nombre { get; set; }

        public ICollection<CallejeroEntity> Vias { get; set; }
    }
}
