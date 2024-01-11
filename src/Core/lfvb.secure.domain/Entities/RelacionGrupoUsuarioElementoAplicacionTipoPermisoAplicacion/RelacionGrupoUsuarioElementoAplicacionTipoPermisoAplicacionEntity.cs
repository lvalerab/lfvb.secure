using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion
{
    public class RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity
    {
        public Guid IdGrupo { get; set; }
        public Guid IdElemento { get; set; }
        public string CodigoTipoPermiso { get; set; }

        public List<GrupoUsuariosAplicacionEntity> Grupos { get; set; }
        public List<ElementoAplicacion.ElementoAplicacionEntity> Elementos { get; set; }
        public List<TipoPermisoElementoAplicacion.TipoPermisoElementoAplicacionEntity> TiposPermisos { get; set; }
    }
}
