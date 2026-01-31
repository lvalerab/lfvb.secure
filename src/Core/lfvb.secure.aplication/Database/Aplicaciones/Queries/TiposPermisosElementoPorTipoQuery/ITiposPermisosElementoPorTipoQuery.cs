using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.TiposPermisosElementoPorTipoQuery
{
    public interface ITiposPermisosElementoPorTipoQuery
    {
        public Task<List<TipoPermisoElementoAplicacionModel>> Execute(string codigoTipoElemento);   
    }
}
