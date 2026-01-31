using AutoMapper;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades
{
    public class GetAllPropiedadesQuery : IGetAllPropiedadesQuery
    {
        private readonly IMapper _mapper;
        private readonly IDataBaseService _db;
        public GetAllPropiedadesQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<List<PropiedadModel>> Execute(string? CodPropiedadPadre = null, string? CodTipoElemento = null)
        {
            List<PropiedadModel> propiedades = new List<PropiedadModel>();
    
            propiedades.AddRange(await (from p in _db.Propiedades
                                        join rp in _db.RelacionesTiposElementosPropiedades on p.Codigo equals rp.CodigoPropiedad
                                        where p.CodigoPadre==CodPropiedadPadre //Que no tenga padre
                                            && ((CodTipoElemento==null) || (CodTipoElemento!=null && rp.CodigoTipoElemento==CodTipoElemento))
                                        select new PropiedadModel
                                        {
                                            Codigo = p.Codigo,
                                            Nombre = p.Nombre,
                                            TipoPropiedad = new TipoPropiedadModel
                                            {
                                                Codigo = p.TipoPropiedad.Codigo,
                                                Nombre = p.TipoPropiedad.Nombre,
                                                Historico = p.TipoPropiedad.Historico == "S",
                                                Intervalo = p.TipoPropiedad.Intervalo == "S",
                                                Multiple = p.TipoPropiedad.Multiple == "S",
                                                ListaValores = p.TipoPropiedad.ListaValores == "S",
                                                Tipo = p.TipoPropiedad.Tipo
                                            }
                                        }).ToListAsync<PropiedadModel>());

            foreach (PropiedadModel p in propiedades)
            {
                p.Propiedades = await this.Execute(p.Codigo, CodTipoElemento);
            }
            return propiedades;
        }
    }
}
