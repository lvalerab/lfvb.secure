using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoTipoPermiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoPermisoElementoAplicacion
{
    public class TipoPermisoElementoAplicacionEntity
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public int Prioridad { get; set; }


        public ICollection<RelacionTipoElementoTipoPermisoEntity> RelacionTiposElementos { get; set; }
        public ICollection<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity> RelacionElementosGrupos { get; set; }
    }
}
