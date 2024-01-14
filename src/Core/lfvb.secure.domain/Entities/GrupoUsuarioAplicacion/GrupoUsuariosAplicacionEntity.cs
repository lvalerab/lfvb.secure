using lfvb.secure.domain.Entities;
using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.GrupoUsuarioAplicacion
{
    public class GrupoUsuariosAplicacionEntity
    {
        public Guid Id { get; set; }

        public Guid? IdAplicacion { get; set; }

        public string Nombre { get; set; }

        public Guid? IdPadre { get; set; }

        public ICollection<RelacionUsuarioGrupoUsuarioAplicacionEntity> RelacionUsuarios { get; set; }

        public GrupoUsuariosAplicacionEntity? Padre { get; set; }

        public ICollection<GrupoUsuariosAplicacionEntity> Hijos { get; set; }

        public AplicacionEntity Aplicacion { get; set; }
        
    }
}
