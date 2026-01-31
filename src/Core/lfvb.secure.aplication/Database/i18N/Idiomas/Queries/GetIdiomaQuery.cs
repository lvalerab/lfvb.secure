using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Queries
{
    public class GetIdiomaQuery:IGetIdiomaQuery
    {
        private IDataBaseService _db;
        private IMapper _mp;
        private IGetPropiedadesElementoQuery _qryGetPropiedades;

        public GetIdiomaQuery(IDataBaseService db, IMapper mp, IGetPropiedadesElementoQuery qryProp)
        {
            _db = db;
            _mp = mp;
            _qryGetPropiedades = qryProp;
        }

        public async Task<IdiomaModel?> execute(string codigo)
        {
            IdiomaModel? rs= await (from i in _db.Idiomas.Include(i=>i.Agrupaciones)
                             where i.Codigo.Equals(codigo)
                             select new IdiomaModel
                             {
                                 Codigo = i.Codigo,
                                 Nombre = i.Nombre,
                                 Multiple = (i.Agrupaciones.Count()>0),                                 
                             }).FirstOrDefaultAsync();

            Guid? id= await (from i in _db.Idiomas
                             where i.Codigo.Equals(codigo)
                             select i.Id).FirstOrDefaultAsync();

            if(rs != null && id!=null)
                rs.Propiedades = await _qryGetPropiedades.Execute(id??Guid.Empty, "ICON_IDIO");

            if(rs.Multiple)
            {
                rs.Componentes = await (from ai in _db.AgrupacionesIdiomas
                                        join idi in _db.Idiomas on ai.CodigoIdiomaRelacionado equals idi.Codigo
                                        where ai.Codigo == rs.Codigo
                                        select new IdiomaModel
                                        {
                                            Codigo = idi.Codigo,
                                            Nombre = idi.Nombre,
                                            Multiple = false,
                                            Orden=ai.Orden
                                        }).ToListAsync();
                foreach(IdiomaModel m in rs.Componentes)
                {
                    id = await (from i in _db.Idiomas
                                      where i.Codigo == m.Codigo
                                      select i.Id).FirstOrDefaultAsync();
                    m.Propiedades= await _qryGetPropiedades.Execute(id ?? Guid.Empty, "ICON_IDIO");
                }
            }
            return rs;
        }
    }
}
