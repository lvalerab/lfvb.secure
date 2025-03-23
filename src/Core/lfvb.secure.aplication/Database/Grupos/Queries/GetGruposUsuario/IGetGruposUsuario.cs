using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario
{
    public interface IGetGruposUsuario
    {
        public Task<List<GetGruposUsuarioModel>?> Execute(Guid? id);
    }
}
