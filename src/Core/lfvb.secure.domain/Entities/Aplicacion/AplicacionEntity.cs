using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;

namespace lfvb.secure.domain.Entities.Aplicacion
{
    public class AplicacionEntity
    {
        public Guid Id { get; set; }

        public string Codigo { get; set; }  

        public string? Nombre { get; set; }

        public virtual ICollection<GrupoUsuariosAplicacionEntity>? Grupos { get; set; }
        
        public virtual ICollection<ElementoAplicacionEntity>? Elementos { get; set; }

    }
}
