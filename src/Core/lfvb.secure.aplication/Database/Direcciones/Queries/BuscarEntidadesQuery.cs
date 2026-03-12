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
    public class BuscarEntidadesQuery: IBuscarEntidadesQuery
    {
        private readonly IDataBaseService _db;

        public BuscarEntidadesQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<EntidadTerritorialModel>> execute(FiltroBusquedaEntidadTerritorialModel filtro)
        {
            List<string> CodigoTipos=new List<string>();
            List<Guid> idPadres=new List<Guid>();

            if (filtro.TiposEntidades != null)
            {
                CodigoTipos = filtro.TiposEntidades.Select(x => x.Codigo).ToList();
            }

            if (filtro.Padres != null)
            {
                idPadres = filtro.Padres.Select(x => x.Id.Value).ToList();
            }

            List<EntidadTerritorialModel> resultado=await (from et in _db.EntidadesTerritoriales
                                                                            .Include(et=>et.TipoEntidad)
                                                                            .Include(et=>et.Padre)
                                                           where (CodigoTipos.Count == 0 || CodigoTipos.Contains(et.TipoEntidad.Codigo))
                                                            && (idPadres.Count == 0 || (et.Padre != null && idPadres.Contains(et.Padre.Id)))
                                                            && (string.IsNullOrEmpty(filtro.Nombre) || et.Nombre.Contains(filtro.Nombre))
                                                              select new EntidadTerritorialModel
                                                              {
                                                                  Id=et.Id,
                                                                  Nombre=et.Nombre,   
                                                                  Tipo=new TipoEntidadTerritorialModel
                                                                    {
                                                                        Id=et.TipoEntidad.Id,
                                                                        Codigo=et.TipoEntidad.Codigo,
                                                                        Nombre=et.TipoEntidad.Nombre
                                                                    },
                                                                  Padre=et.Padre != null ? new EntidadTerritorialModel
                                                                        {
                                                                            Id=et.Padre.Id,
                                                                            Nombre=et.Padre.Nombre,
                                                                            Tipo=new TipoEntidadTerritorialModel
                                                                            {
                                                                                Id=et.Padre.TipoEntidad.Id,
                                                                                Codigo=et.Padre.TipoEntidad.Codigo,
                                                                                Nombre=et.Padre.TipoEntidad.Nombre
                                                                            }
                                                                        } : null    
                                                              }).ToListAsync();
            return resultado;
        }
    }
}
