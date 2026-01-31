using lfvb.secure.domain.Entities.Credencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Models
{
    public class CreateUsuarioModel
    {
        public Guid? IdNuevo { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }
    }
}
