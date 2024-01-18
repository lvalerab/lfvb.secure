using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso
{
    public class RelacionTipoElementoTipoPermisoEntity
    {
        public string CodigoTipoElemento { get; set; }
        public string CodigoTipoPermiso { get; set; }

        public TipoElementoAplicacionEntity TipoElemento { get; set; }
        public TipoPermisoElementoAplicacionEntity TipoPermiso { get; set; }
    }
}
