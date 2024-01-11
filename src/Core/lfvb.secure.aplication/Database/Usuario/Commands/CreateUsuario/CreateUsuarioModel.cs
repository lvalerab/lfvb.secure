using lfvb.secure.domain.Entities.Credencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario
{
    public class CreateUsuarioModel
    {
        public Guid? IdNuevo { get; set; }

        public String Nombre { get; set; }

        public String Apellido1 { get; set; }

        public String Apellido2 { get; set; }

        public String Usuario { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }
    }
}
