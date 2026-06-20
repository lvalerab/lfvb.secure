using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class CalendarioUsuarioEntity
    {
        public Guid Id { get; set; }        
        public Guid IdUsuario { get; set; }
        public String Nombre { get; set; }
        
        public UsuarioEntity Usuario { get; set; }

        public ICollection<CalendarioUsuarioEntradasEntity> Entradas { get; set; }
    }
}
