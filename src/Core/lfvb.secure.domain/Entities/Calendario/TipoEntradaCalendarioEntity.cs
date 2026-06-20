using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class TipoEntradaCalendarioEntity
    {
        public Guid Id { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }

        public ICollection<EntradaCalendarioEntity> EntradasCalendario { get; set; }
    }
}
