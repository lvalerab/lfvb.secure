using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetGruposAplicacion
{
    public interface IGetGruposAplicacionQuery
    {
        public Task<List<GrupoModel>> Execute(Guid? id);
    }
}
