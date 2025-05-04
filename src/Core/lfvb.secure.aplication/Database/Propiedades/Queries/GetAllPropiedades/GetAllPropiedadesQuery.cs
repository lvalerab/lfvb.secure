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

        public async Task<List<PropiedadModel>> Execute(string CodPropiedadPadre = null)
        {
            List<PropiedadModel> propiedades = await(from p in _db.Propiedades
                                                     where (CodPropiedadPadre == null || (CodPropiedadPadre!=null && p.PropiedadPadre.Codigo.Equals(CodPropiedadPadre)))
                                                     select new PropiedadModel
                                                     {
                                                         Codigo = p.Codigo,
                                                         Nombre = p.Nombre,
                                                         TipoPropiedad = new TipoPropiedadModel
                                                         {
                                                             Codigo = p.TipoPropiedad.Codigo,
                                                             Nombre = p.TipoPropiedad.Nombre
                                                         }
                                                     }).ToListAsync<PropiedadModel>();
            return propiedades;
        }   
    }
}
