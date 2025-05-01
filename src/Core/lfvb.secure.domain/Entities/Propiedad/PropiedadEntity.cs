using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.TipoPropiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Propiedad
{
    public class PropiedadEntity
    {
        public String Codigo { get; set; }
        public String? CodigoPadre { get; set; } 
        public PropiedadEntity? PropiedadPadre { get; set; }
        public String? CodTipoPropiedad { get; set; }
        public TipoPropiedadEntity TipoPropiedad { get; set; }
        public String? Nombre { get; set; }

        public virtual ICollection<PropiedadEntity> PropiedadesHijas { get; set; }
        public virtual ICollection<PropiedadElementoEntity>? PropiedadesElementos { get; set; } 
    }
}
