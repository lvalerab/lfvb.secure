using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class TareaEntity
    {
        public Guid Id { get; set; }
        public Guid IdPropietario { get; set; } 
        public Guid IdTipoTarea { get; set; }
        public string Nombre { get; set; }  
        public string Descripcion { get; set; }

        public UsuarioEntity Propietario { get; set; }  
        public TipoTareaEntity TipoTarea { get; set; }
    }
}
