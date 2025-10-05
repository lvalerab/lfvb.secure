
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;

namespace lfvb.secure.domain.Entities.Circuitos.PermisoPasoGrupo
{
    public class PermisoPasoGrupoEntity
    {
        public Guid IdPaso { get; set; }
        public Guid IdGrupoUsuario { get; set; }

        public PasoEntity Paso { get; set; }
        public GrupoUsuariosAplicacionEntity GrupoUsuario { get; set; } 
    }
}
