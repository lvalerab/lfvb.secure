using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetUsuariosGrupo
{
    public interface IGetUsuariosGrupoQuery
    {
        public Task<List<UsuarioModel>> Execute(Guid grupoId);
    }
}
