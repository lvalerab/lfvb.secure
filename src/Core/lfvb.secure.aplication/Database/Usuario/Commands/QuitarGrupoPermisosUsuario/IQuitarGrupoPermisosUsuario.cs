using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.QuitarGrupoPermisosUsuario
{
    public interface IQuitarGrupoPermisosUsuario
    {
        public Task<bool> Execute(Guid idUsuario, List<Guid> grupos);
    }
}
