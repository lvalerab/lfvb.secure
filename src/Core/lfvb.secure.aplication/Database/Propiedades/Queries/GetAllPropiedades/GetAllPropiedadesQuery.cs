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
    public class GetAllPropiedadesQuery:IGetAllPropiedadesQuery
    {
        private readonly IMapper _mapper;
        private readonly IDataBaseService _db;
        public GetAllPropiedadesQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<List<PropiedadModel>> Execute(string? CodPropiedadPadre = null, string? CodTipoElemento =null)
        {
            //Propiedades raiz
            List<PropiedadModel> propiedades = await(from p in _db.Propiedades
                                                     where (CodPropiedadPadre == null || (CodPropiedadPadre!=null && p.PropiedadPadre.Codigo.Equals(CodPropiedadPadre)))
                                                     && ((CodTipoElemento == null) || (CodTipoElemento != null && p.RelacionTiposElementos.Any(x=>x.CodigoTipoElemento.Equals(CodTipoElemento))))
                                                     && _db.Propiedades.Where(x=>x.CodigoPadre==p.Codigo).Any()==false //Que no tenga hijas
                                                     select new PropiedadModel
                                                     {
                                                         Codigo = p.Codigo,
                                                         Nombre = p.Nombre,
                                                         TipoPropiedad = new TipoPropiedadModel
                                                         {
                                                             Codigo = p.TipoPropiedad.Codigo,
                                                             Nombre = p.TipoPropiedad.Nombre,
                                                             Historico=p.TipoPropiedad.Historico=="S",
                                                             Intervalo=p.TipoPropiedad.Intervalo=="S",
                                                             Multiple=p.TipoPropiedad.Multiple=="S",
                                                             Tipo=p.TipoPropiedad.Tipo
                                                         }
                                                     }).ToListAsync<PropiedadModel>();

            List<PropiedadModel> propiedadesPadres = await (from p in _db.Propiedades
                                                            where (CodPropiedadPadre == null || (CodPropiedadPadre != null && p.PropiedadPadre.Codigo.Equals(CodPropiedadPadre)))
                                                            && ((CodTipoElemento == null) || (CodTipoElemento != null && p.RelacionTiposElementos.Any(x => x.CodigoTipoElemento.Equals(CodTipoElemento))))
                                                            && _db.Propiedades.Where(x => x.CodigoPadre == p.Codigo).Any() == true //Que tenga hijas
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
                                                                    Tipo = p.TipoPropiedad.Tipo
                                                                }
                                                            }).ToListAsync<PropiedadModel>();
            foreach(PropiedadModel p in propiedadesPadres)
            {
               p.Propiedades=await this.Execute(p.Codigo, CodTipoElemento);
            }

            propiedades.AddRange(propiedadesPadres);
            return propiedades;
        }   
    }
}
