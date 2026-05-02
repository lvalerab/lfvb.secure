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
    public class GetArbolEntidadTerritorialQuery: IGetArbolEntidadTerritorialQuery
    {
        private readonly IDataBaseService _db;

        public GetArbolEntidadTerritorialQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<EntidadTerritorialModel> execute(Guid id)
        {
            EntidadTerritorialModel enti=await (from et in _db.EntidadesTerritoriales.Include(t=>t.TipoEntidad)    
                                                where et.Id == id
                                                select new EntidadTerritorialModel
                                                {
                                                    Id = et.Id,
                                                    Nombre = et.Nombre,
                                                    Tipo = new TipoEntidadTerritorialModel
                                                    {
                                                        Id = et.TipoEntidad.Id,
                                                        Codigo = et.TipoEntidad.Codigo,
                                                        Nombre = et.TipoEntidad.Nombre
                                                    },
                                                    Padre = new EntidadTerritorialModel
                                                    {
                                                        Id = et.Padre.Id                                
                                                    }
                                                }).FirstOrDefaultAsync();

            if(enti!=null && enti.Padre != null && enti.Padre.Id!=null)
            {
                enti.Padre = await execute(enti.Padre.Id.Value);
            }

            return enti;
        }
    }
}
