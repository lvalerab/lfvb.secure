using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetGruposAplicacion;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacion
{
    public class GetAplicacionQuery: IGetAplicacionQuery
    {
        private IDataBaseService _db;
        private IMapper _mapper;
        private IGetArbolElementosAplicacion _getArbolElementosAplicacion;
        private IGetGruposAplicacionQuery _qryGetGruposAplicacion;

        public GetAplicacionQuery(IDataBaseService db, IMapper mapper, IGetArbolElementosAplicacion qryArbolElementosAplicacion, IGetGruposAplicacionQuery qryGruposAplicacion)
        {
            _db = db;
            _mapper = mapper;
            _getArbolElementosAplicacion = qryArbolElementosAplicacion;
            _qryGetGruposAplicacion = qryGruposAplicacion;
        }

        public async Task<AplicacionModel> Execute(Guid idAplicacion)
        {
           AplicacionModel app=await (from ap in _db.Aplicaciones
                                      where ap.Id == idAplicacion
                                      select new AplicacionModel
                                      {
                                          Id = ap.Id,
                                          Codigo = ap.Codigo,
                                          Nombre = ap.Nombre
                                      }).FirstOrDefaultAsync<AplicacionModel>();

            app.Elementos = await _getArbolElementosAplicacion.Execute(idAplicacion);  
            app.Grupos = await _qryGetGruposAplicacion.Execute(idAplicacion);   

            return app;
        }   
    }
}
