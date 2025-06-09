using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario
{
    public interface ICreateUsuarioCommand
    {
        public Task<CreateUsuarioModel> Execute(CreateUsuarioModel usuario);
    }
}
