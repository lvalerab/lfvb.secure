using lfvb.secure.domain.Entities.TipoElementoAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.RelacionTipoElementoApliConTipoPermisoTipoElementoApli
{
    public class RelacionTipoElementoApliConTipoPermisoTipoElementoApliEntity
    {
        public string CodigoTipoElemento { get; set; }
        public string CodigoTipoPermiso { get; set; }

        public List<TipoElementoAplicacion.TipoElementoAplicacionEntity> TiposElementos { get; set; }
        public List<TipoPermisoElementoAplicacion.TipoPermisoElementoAplicacionEntity> TipoPermisos { get; set; }

    }
}
