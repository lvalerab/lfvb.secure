using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario
{
    public class PermisoPasoUsuarioEntity
    {
        public Guid IdPaso { get; set; }
        public Guid IdUsuario { get; set; }
        
        public PasoEntity Paso { get; set; }
        public UsuarioEntity Usuario { get; set; }  
    }
}
