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

        public GrupoUsuariosAplicacionEntity Grupo { get; set; }
        public ElementoAplicacionEntity ElementoAplicacion { get; set; }
        public TipoPermisoElementoAplicacionEntity TipoPermiso { get; set; }
    }
}
