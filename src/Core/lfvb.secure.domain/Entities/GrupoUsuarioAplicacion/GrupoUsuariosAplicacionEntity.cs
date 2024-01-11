using lfvb.secure.domain.Entities;
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

        public List<GrupoUsuariosAplicacionEntity> Descendientes { get; set; }

        public GrupoUsuariosAplicacionEntity? Padre { get; set; }

        public Aplicacion.AplicacionEntity? Aplicacion { get; set; }
        
    }
}
