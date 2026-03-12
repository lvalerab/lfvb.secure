using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Queries
{
    public class GetAllTiposEntidadesTerritorialesQuery: IGetAllTiposEntidadesTerritorialesQuery
    {
        private readonly IDataBaseService _db;

        public GetAllTiposEntidadesTerritorialesQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<TipoEntidadTerritorialModel>> execute()
        {
            List<TipoEntidadTerritorialModel> result = await (from te in _db.TiposEntidadesTerritoriales
                                                              select new TipoEntidadTerritorialModel
                                                              {
                                                                  Id = te.Id,
                                                                  Codigo = te.Codigo,
                                                                  Nombre = te.Nombre
                                                              }).ToListAsync(); 
            return result;
        }
    }
}
