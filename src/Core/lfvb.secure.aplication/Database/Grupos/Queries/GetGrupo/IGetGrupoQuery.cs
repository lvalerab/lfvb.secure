using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetGrupo
{
    public interface IGetGrupoQuery
    {
        public Task<GrupoModel?> Execute(Guid grupoId);
    }
}
