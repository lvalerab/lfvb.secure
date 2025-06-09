using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.ActualizaUsuario
{
    public interface IActualizaUsuarioCommand
    {
        public Task<string> Validate(ActualizaUsuarioModel data);

        public Task<ActualizaUsuarioModel> Execute(ActualizaUsuarioModel data);
    }
}
