using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisosElementosAplicacionPorGrupoYAplicacion
{
    public interface IPermisosElementosAplicacionPorGrupoYAplicacionQuery
    {
        public Task<List<PermisoElementoAplicacionModel>> Execute(Guid idAplicacion, Guid idGrupo);
    }
}
