using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario
{
    public class UpdateUsuarioModel
    {
        public Guid Id { get; set; }

        public String Nombre { get; set; }

        public String Apellido1 { get; set; }

        public String Apellido2 { get; set; }
    }
}
