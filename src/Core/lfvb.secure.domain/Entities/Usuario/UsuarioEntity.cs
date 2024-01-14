using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Usuario
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }

        public String Nombre { get; set; }

        public String Apellido1 { get; set; }

        public String Apellido2 { get; set; }

        public String Usuario { get; set; }

        public ICollection<CredencialEntity> Credenciales { get; set; }

        public ICollection<RelacionUsuarioGrupoUsuarioAplicacionEntity> RelacionGrupos { get; set; }

    }
}
