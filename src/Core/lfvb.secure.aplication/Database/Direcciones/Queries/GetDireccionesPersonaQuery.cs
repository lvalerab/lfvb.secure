using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Queries
{
    public class GetDireccionesPersonaQuery: IGetDireccionesPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetDireccionesPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<DireccionModel>> execute(Guid idPersona)
        {
            //Obtenemos las direcciones normalizadas
            List<DireccionModel> dirNormalizadas = await (from ep in _db.ElementosPersona
                                                          from dr in _db.Direcciones.Include(d => d.DireccionNormalizada)
                                                          from cl in _db.Callejeros.Include(c => c.EntidadTerritorial).Include(c=>c.TipoVia)
                                                          from te in _db.TiposEntidadesTerritoriales
                                                          where ep.IdElemento == dr.Id
                                                            && dr.DireccionNormalizada!=null
                                                            && dr.DireccionNormalizada.IdCalle == cl.Id
                                                            && cl.EntidadTerritorial.CodigoTipoEntidad==te.Codigo
                                                            && ep.IdPersona == idPersona
                                                          select new DireccionModel
                                                          {
                                                              Id = dr.Id,
                                                              Normalizada = new DireccionNormalizadaModel
                                                              {
                                                                  Calle = new CallejeroModel
                                                                  {
                                                                      Id=cl.Id,
                                                                      EntidadTerritorial=new EntidadTerritorialModel
                                                                      {
                                                                          Id=cl.EntidadTerritorial.Id,
                                                                          Nombre=cl.EntidadTerritorial.Nombre,
                                                                          Tipo=new TipoEntidadTerritorialModel
                                                                          {
                                                                              Codigo=te.Codigo,
                                                                              Id=te.Id,
                                                                              Nombre=te.Nombre
                                                                          }
                                                                      },
                                                                      TipoVia=new TipoViaModel
                                                                      {
                                                                        Codigo=cl.TipoVia.Codigo,
                                                                        Nombre=cl.TipoVia.Nombre
                                                                      },
                                                                      Nombre=cl.Nombre
                                                                  },
                                                                  Numero=dr.DireccionNormalizada.Numero,
                                                                  Ampliacion=dr.DireccionNormalizada.Ampliacion,
                                                                  Bloque=dr.DireccionNormalizada.Bloque,
                                                                  Edificio= dr.DireccionNormalizada.Edificio,
                                                                  Escalera= dr.DireccionNormalizada.Escalera,
                                                                  Piso = dr.DireccionNormalizada.Piso,
                                                                  Puerta = dr.DireccionNormalizada.Puerta
                                                              }
                                                          }).ToListAsync();

            ///Obtenemos las direcciones no normalizadas de la persona
            List<DireccionModel> dirNoNorm = await (from ep in _db.ElementosPersona
                                                    from dr in _db.Direcciones.Include(d => d.DireccionNoNormalizada)
                                                    from et in _db.EntidadesTerritoriales.Include(et => et.TipoEntidad)
                                                    where ep.IdPersona == idPersona
                                                      && ep.IdElemento == dr.Id
                                                      && dr.DireccionNoNormalizada != null
                                                      && dr.DireccionNoNormalizada.IdEntidadTerritorial == et.Id
                                                    select new DireccionModel
                                                    {
                                                        Id = dr.Id,
                                                        NoNormalizada = new DireccionNoNormalizadaModel
                                                        {
                                                            Calle = dr.DireccionNoNormalizada.IdCalle != null ? (from cl in _db.Callejeros.Include(c => c.EntidadTerritorial).Include(c => c.TipoVia)
                                                                                                                  where cl.Id == dr.DireccionNoNormalizada.IdCalle
                                                                                                                  select new CallejeroModel
                                                                                                                  {
                                                                                                                      Id = cl.Id,
                                                                                                                      EntidadTerritorial = new EntidadTerritorialModel
                                                                                                                      {
                                                                                                                          Id = cl.EntidadTerritorial.Id,
                                                                                                                          Nombre = cl.EntidadTerritorial.Nombre,
                                                                                                                          Tipo = new TipoEntidadTerritorialModel
                                                                                                                          {
                                                                                                                              Codigo = cl.TipoVia.Codigo,
                                                                                                                              Nombre = cl.TipoVia.Nombre
                                                                                                                          }
                                                                                                                      },
                                                                                                                      TipoVia = new TipoViaModel
                                                                                                                      {
                                                                                                                          Codigo = cl.TipoVia.Codigo,
                                                                                                                          Nombre = cl.TipoVia.Nombre
                                                                                                                      },
                                                                                                                      Nombre = cl.Nombre
                                                                                                                  }).FirstOrDefault() : null,
                                                            Entidad = new EntidadTerritorialModel
                                                            {
                                                                Id = et.Id,
                                                                Nombre = et.Nombre,
                                                                Tipo = new TipoEntidadTerritorialModel
                                                                {
                                                                    Codigo = et.TipoEntidad.Codigo,
                                                                    Nombre = et.TipoEntidad.Nombre
                                                                }
                                                            },
                                                            Linea1=dr.DireccionNoNormalizada.Linea1,
                                                            Linea2=dr.DireccionNoNormalizada.Linea2,
                                                            Linea3=dr.DireccionNoNormalizada.Linea3
                                                        }
                                                    }).ToListAsync();

            List<DireccionModel> resultado = new List<DireccionModel>();
            resultado.AddRange(dirNormalizadas);
            resultado.AddRange(dirNoNorm);
            return resultado;
        }
    }
}
