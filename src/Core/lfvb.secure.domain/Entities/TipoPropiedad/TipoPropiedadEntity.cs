using lfvb.secure.domain.Entities.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoPropiedad
{
    public class TipoPropiedadEntity
    {
        public String Codigo { get; set; }
        public String? Nombre { get; set; }
        public String Multiple { get; set; }
        public String Historico { get; set; }
        public String Intervalo { get; set; }
        public String Tipo { get; set; }
        public String ListaValores { get; set; }


        public virtual ICollection<PropiedadEntity>? Propiedades { get; set; }
    }
}
