using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario
{
    public interface IUpdateUsuarioCommand
    {
        public Task<UpdateUsuarioModel> Execute(UpdateUsuarioModel model);
    }
}
