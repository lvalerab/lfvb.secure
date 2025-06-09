using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Models
{
    public class ActualizaUsuarioModel
    {
        public Guid? Id { get; set; }
        public string? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Email { get; set; }
    }
}
