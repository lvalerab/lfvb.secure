using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.TiposPermisosElementoPorTipoQuery
{
    public class TiposPermisosElementoPorTipoQuery: ITiposPermisosElementoPorTipoQuery
    {
        private readonly IDataBaseService _db;
        
        public TiposPermisosElementoPorTipoQuery(IDataBaseService db)
        {
            this._db = db;
        }
        public async Task<List<TipoPermisoElementoAplicacionModel>> Execute(string codigoTipoElemento)
        {
            return await (from tp in _db.TiposPermisosTipoElementosAplicaciones.Include(t => t.RelacionTiposElementos)
                          where tp.RelacionTiposElementos.Any(r => r.CodigoTipoElemento.Equals(codigoTipoElemento))
                          select new TipoPermisoElementoAplicacionModel
                          {
                              Codigo = tp.Codigo,
                              Nombre = tp.Nombre
                          }).ToListAsync<TipoPermisoElementoAplicacionModel>();
        }
    }
}
