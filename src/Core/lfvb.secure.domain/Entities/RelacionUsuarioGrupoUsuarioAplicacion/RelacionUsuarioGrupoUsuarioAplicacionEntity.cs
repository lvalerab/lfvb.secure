using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion
{
    public class RelacionUsuarioGrupoUsuarioAplicacionEntity
    {
        public Guid IdUsuario { get; set; }
        public Guid IdGrupo { get; set; }

        public UsuarioEntity Usuario { get; set; }
        public GrupoUsuariosAplicacionEntity Grupo { get; set; }    
    }
}
