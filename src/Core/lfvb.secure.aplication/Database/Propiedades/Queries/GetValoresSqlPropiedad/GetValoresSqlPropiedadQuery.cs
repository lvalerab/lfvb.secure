using AutoMapper;
using lfvb.secure.aplication.Database.Propiedades.Models;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.PropiedadValoresSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetValoresSqlPropiedad
{
    public class GetValoresSqlPropiedadQuery : IGetValoresSqlPropiedadQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        
        public GetValoresSqlPropiedadQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<List<GrupoValorEtiquetaModel>> execute(string codigoPropiedad, string idElemento)
        {
            //Obtenemos el tipo de propided
            List<GrupoValorEtiquetaModel> resultado = new List<GrupoValorEtiquetaModel>();

            TipoPropiedadModel tipoProps=await (from tp in _db.TiposPropiedades
                                         join pr in _db.Propiedades on tp.Codigo equals pr.CodTipoPropiedad
                                         where pr.Codigo== codigoPropiedad  
                                         select new TipoPropiedadModel
                                        {
                                            Codigo = tp.Codigo,
                                            Nombre = tp.Nombre,
                                            Multiple = tp.Multiple.Equals("S"),
                                            Historico = tp.Historico.Equals("S"),
                                            Intervalo = tp.Intervalo.Equals("S"),
                                            Tipo = tp.Tipo,
                                            ListaValores = tp.ListaValores.Equals("S")
                                        }).FirstOrDefaultAsync<TipoPropiedadModel>();

            if (tipoProps == null || !tipoProps.ListaValores)
            {
                 return new List<GrupoValorEtiquetaModel>();
            } else
            {
                List<PropiedadValoresSqlEntity> propiedadValoresSql = await (from pvs in _db.PropiedadesValoresSql
                                                                 where pvs.Codigo == codigoPropiedad
                                                                 select pvs).ToListAsync();

                foreach(var sql in propiedadValoresSql)
                {
                    GrupoValorEtiquetaModel grupo = new GrupoValorEtiquetaModel
                    {
                        Value = sql.Etiqueta,
                        Label = sql.Codigo+" "+sql.Etiqueta,
                        Items = new List<ValorEtiquetaModel>()
                    };
                    if (sql.FiltrarPorId.Equals("S")) { 
                        grupo.Items =await _db.FromSql<ValorEtiquetaModel>(sql.Sql.Replace("@ID_ELEMENTO", $"'{idElemento}'")).ToListAsync();
                    } else
                    {
                        grupo.Items = await _db.FromSql<ValorEtiquetaModel>(sql.Sql).ToListAsync();
                    }
                    resultado.Add(grupo);   
                }
                return resultado;
            }
        }
    }
}
