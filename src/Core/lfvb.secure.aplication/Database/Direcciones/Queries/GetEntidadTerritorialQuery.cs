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
    public class GetEntidadTerritorialQuery: IGetEntidadTerritorialQuery
    {
        private readonly IDataBaseService _db;
        public GetEntidadTerritorialQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<EntidadTerritorialModel?> execute(Guid id)
        {
            EntidadTerritorialModel? resultado=await (from et in _db.EntidadesTerritoriales
                                                                        .Include(et=>et.TipoEntidad)
                                                                        .Include(et=>et.Padre)
                                                      select new EntidadTerritorialModel
                                                      {
                                                          Id=et.Id,
                                                          Nombre=et.Nombre,
                                                          Tipo=new TipoEntidadTerritorialModel
                                                          {
                                                              Id=et.TipoEntidad.Id,
                                                              Nombre=et.TipoEntidad.Nombre
                                                          },
                                                          Padre=et.Padre==null?null:new EntidadTerritorialModel
                                                          {
                                                              Id=et.Padre.Id,
                                                              Nombre=et.Padre.Nombre,
                                                              Tipo=new TipoEntidadTerritorialModel
                                                              {
                                                                  Id=et.Padre.TipoEntidad.Id,
                                                                  Nombre=et.Padre.TipoEntidad.Nombre
                                                              }
                                                          }
                                                      }).FirstOrDefaultAsync();

            return resultado;
        }
    }
}
