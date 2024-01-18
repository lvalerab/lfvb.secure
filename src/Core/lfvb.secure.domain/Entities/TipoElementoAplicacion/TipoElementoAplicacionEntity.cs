using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoElementoAplicacion
{
    public class TipoElementoAplicacionEntity
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public ICollection<ElementoAplicacionEntity> Elementos { get; set; }
        public ICollection<RelacionTipoElementoTipoPermisoEntity> RelacionTiposPermisos { get; set; }
    }
}
