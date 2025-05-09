using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.RelacionTipoElementoPropiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoElemento
{
    public class TipoElementoEntity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public ICollection<ElementoEntity> Elementos { get; set; }
        public ICollection<RelacionTipoElementoPropiedadEntity> RelacionPropiedades { get; set; }
    }
}
