using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Commands.AltaGrupoUsuariosPermisos
{
    public interface IAltaGrupoUsuariosPermisosCommand
    {
        public Task<GrupoModel> Execute(GrupoModel grupo);
    }
}
