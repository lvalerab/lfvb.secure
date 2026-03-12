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
    public class BuscadorCallejeroQuery: IBuscadorCallejeroQuery
    {
        private readonly IDataBaseService _db;

        public BuscadorCallejeroQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<CallejeroModel>> execute(FiltroBusquedaCallejeroModel filtro)
        {
            List<Guid?> entidades=new List<Guid?>();
            if (filtro.EntidadesTerritoriales != null)
            {
                entidades = filtro.EntidadesTerritoriales.Select(e => e.Id).ToList();
            }
            List<string> tiposVias = new List<string>();
            if (filtro.TiposVia != null)
            {
                tiposVias = filtro.TiposVia.Select(t => t.Nombre).ToList();
            }

            List<Guid> callesSup=new List<Guid>();
            if (filtro.CallesSuperiores != null)
            {
                callesSup = filtro.CallesSuperiores.Select(c => c.Id ?? Guid.Empty).ToList();
            }

            List<Guid> callesInf=new List<Guid>();
            if (filtro.CallesInferiores != null)
            {
                callesInf = filtro.CallesInferiores.Select(c => c.Id ?? Guid.Empty).ToList();
            }

            List<CallejeroModel> resultado=await (from cl in _db.Callejeros
                                                                    .Include(c=>c.TipoVia)
                                                                    .Include(c=>c.EntidadTerritorial)   
                                                                    .Include(c=>c.CalleSuperior)    
                                                                    .Include(c=>c.CallesInferiores)
                                                  where (filtro.Nombre == null || cl.Nombre.Contains(filtro.Nombre)) &&
                          (entidades.Count == 0 || entidades.Contains(cl.EntidadTerritorial.Id)) &&
                          (tiposVias.Count == 0 || tiposVias.Contains(cl.TipoVia.Nombre)) &&
                          (callesSup.Count == 0 || (cl.CalleSuperior != null && callesSup.Contains(cl.CalleSuperior.Id))) &&
                          (callesInf.Count == 0 || (cl.CallesInferiores != null && cl.CallesInferiores.Any(ci => callesInf.Contains(ci.Id ))))
                    select new CallejeroModel
                    {
                        Id=cl.Id,
                        Nombre=cl.Nombre,
                        EntidadTerritorial=new EntidadTerritorialModel
                        {
                            Id=cl.EntidadTerritorial.Id,
                            Nombre=cl.EntidadTerritorial.Nombre
                        },
                        TipoVia=new TipoViaModel
                        {
                            Codigo=cl.TipoVia.Codigo,
                            Nombre =cl.TipoVia.Nombre
                        },
                        CalleSuperior=cl.CalleSuperior!=null?new CallejeroModel
                        {
                            Id=cl.CalleSuperior.Id,
                            TipoVia=(from tv in _db.TiposVias                                     
                                     where tv.Codigo == cl.CalleSuperior.TipoVia.Codigo
                                     select new TipoViaModel
                                     {
                                         Codigo = tv.Codigo,
                                         Nombre = tv.Nombre
                                     }).FirstOrDefault(),   
                            Nombre =cl.CalleSuperior.Nombre
                        }:null,
                        CallesInferiores=cl.CallesInferiores!=null?cl.CallesInferiores.Select(ci=>new CallejeroModel
                        {
                            Id=ci.Id,
                            Nombre=ci.Nombre
                        }).ToList(): null
                    }).ToListAsync();   

            return resultado;
        }
    }
}
