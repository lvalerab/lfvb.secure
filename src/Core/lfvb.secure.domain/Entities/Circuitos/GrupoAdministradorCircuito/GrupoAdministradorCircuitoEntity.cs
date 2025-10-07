using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.GrupoAdministradorCircuito
{
    public class GrupoAdministradorCircuitoEntity
    {
        public Guid IdGuap { get; set; }
        public Guid IdCircuito { get; set; }

        public GrupoUsuariosAplicacionEntity GrupoUsuarioAplicacion { get; set; } 
        public CircuitoEntity Circuito { get; set; }
    }
}
