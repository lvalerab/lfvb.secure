using lfvb.secure.domain.Entities.ElementoAplicacion;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Aplicacion
{
    public class AplicacionEntity
    {
        public Guid Id { get; set; }

        public string? Nombre { get; set; }

        public virtual ICollection<GrupoUsuariosAplicacionEntity>? Grupos { get; set; }
        
        public virtual ICollection<ElementoAplicacionEntity>? Elementos { get; set; }

    }
}
