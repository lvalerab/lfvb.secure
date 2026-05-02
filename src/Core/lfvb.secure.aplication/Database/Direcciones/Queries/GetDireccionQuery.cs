using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Queries
{
    public class GetDireccionQuery: IGetDireccionQuery
    {
        private readonly IDataBaseService _db;
        private readonly IGetArbolEntidadTerritorialQuery _getArbolEntidadTerritorialQuery; 

        public GetDireccionQuery(IDataBaseService db, IGetArbolEntidadTerritorialQuery qryArbolEntidad)
        {
            _db = db;
            _getArbolEntidadTerritorialQuery= qryArbolEntidad;
        }

        public async Task<DireccionModel> execute(Guid id)
        {
            DireccionModel resultado=await (from d in _db.Direcciones
                                            where d.Id == id
                                            select new DireccionModel
                                            {
                                                Id = d.Id
                                            }).FirstOrDefaultAsync();
            if (resultado != null)
            {
                resultado.Normalizada = await (from dn in _db.DireccionesNormalizadas
                                                                .Include(d=>d.Callejero)
                                                                .Include(d=>d.Callejero.EntidadTerritorial)
                                                                .Include(d=>d.Callejero.EntidadTerritorial.TipoEntidad)
                                                                .Include(d=>d.Callejero.TipoVia)
                                               where dn.Id == resultado.Id
                                              select new DireccionNormalizadaModel
                                              {
                                                 Calle = new CallejeroModel
                                                 {
                                                     Id = dn.Callejero.Id,
                                                     Nombre = dn.Callejero.Nombre,                                                     
                                                     EntidadTerritorial = new EntidadTerritorialModel
                                                     {
                                                         Id = dn.Callejero.EntidadTerritorial.Id,
                                                         Nombre = dn.Callejero.EntidadTerritorial.Nombre,
                                                         Tipo=new TipoEntidadTerritorialModel
                                                         {
                                                             Id=dn.Callejero.EntidadTerritorial.TipoEntidad.Id,
                                                             Codigo=dn.Callejero.EntidadTerritorial.TipoEntidad.Codigo,
                                                             Nombre=dn.Callejero.EntidadTerritorial.TipoEntidad.Nombre
                                                         },
                                                         Padre=new EntidadTerritorialModel
                                                         {
                                                             Id=dn.Callejero.EntidadTerritorial.Padre.Id,
                                                             Nombre=dn.Callejero.EntidadTerritorial.Padre.Nombre
                                                         }
                                                     },
                                                     TipoVia = new TipoViaModel
                                                     {
                                                         Codigo = dn.Callejero.TipoVia.Codigo,
                                                         Nombre = dn.Callejero.TipoVia.Nombre
                                                     }
                                                 },
                                                    Edificio = dn.Edificio,
                                                    Numero = dn.Numero,
                                                    Puerta = dn.Puerta,
                                                    Piso = dn.Piso,
                                                    Escalera = dn.Escalera,
                                                    Bloque = dn.Bloque,
                                                    Ampliacion = dn.Ampliacion
                                              }).FirstOrDefaultAsync();
                if(resultado.Normalizada == null) { 
                    resultado.NoNormalizada = await (from dnn in _db.DireccionesNoNormalizadas
                                                                        .Include(d=>d.Callejero)
                                                                        .Include(d=>d.Callejero.EntidadTerritorial) 
                                                                        .Include(d=>d.Callejero.EntidadTerritorial.TipoEntidad)
                                                                        .Include(d=>d.Callejero.TipoVia)    
                                                                        .Include(d=>d.EntidadTerritorial)  
                                                                        .Include(d=>d.EntidadTerritorial.TipoEntidad)
                                                     where dnn.Id == resultado.Id
                                                select new DireccionNoNormalizadaModel
                                                {
                                                   Calle = new CallejeroModel
                                                   {
                                                       Id = dnn.Callejero.Id,
                                                       Nombre = dnn.Callejero.Nombre,
                                                       EntidadTerritorial = new EntidadTerritorialModel
                                                       {
                                                           Id = dnn.Callejero.EntidadTerritorial.Id,
                                                           Nombre = dnn.Callejero.EntidadTerritorial.Nombre,
                                                           Tipo=new TipoEntidadTerritorialModel
                                                           {
                                                               Id=dnn.Callejero.EntidadTerritorial.TipoEntidad.Id,
                                                               Codigo=dnn.Callejero.EntidadTerritorial.TipoEntidad.Codigo,
                                                               Nombre=dnn.Callejero.EntidadTerritorial.TipoEntidad.Nombre
                                                           }
                                                       },
                                                       TipoVia = new TipoViaModel
                                                       {
                                                           Codigo = dnn.Callejero.TipoVia.Codigo,
                                                           Nombre = dnn.Callejero.TipoVia.Nombre
                                                       }
                                                   },
                                                   Entidad = new EntidadTerritorialModel
                                                   {
                                                       Id = dnn.EntidadTerritorial.Id,
                                                       Nombre = dnn.EntidadTerritorial.Nombre,
                                                       Tipo=new TipoEntidadTerritorialModel
                                                       {
                                                           Id=dnn.EntidadTerritorial.TipoEntidad.Id,
                                                           Codigo=dnn.EntidadTerritorial.TipoEntidad.Codigo,
                                                           Nombre=dnn.EntidadTerritorial.TipoEntidad.Nombre
                                                       }
                                                   },
                                                    Linea1 = dnn.Linea1,
                                                    Linea2 = dnn.Linea2,
                                                    Linea3 = dnn.Linea3
                                                }).FirstOrDefaultAsync();
                } else
                {
                    resultado.Normalizada.Calle.EntidadTerritorial.Padre = await _getArbolEntidadTerritorialQuery.execute(resultado.Normalizada?.Calle?.EntidadTerritorial?.Padre?.Id??Guid.Empty);
                }
            }

            return resultado;
        }
    }
}
