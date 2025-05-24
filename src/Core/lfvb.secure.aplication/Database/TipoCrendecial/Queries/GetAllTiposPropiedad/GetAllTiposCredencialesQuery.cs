using AutoMapper;
using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoCrendecial.Queries.GetAllTiposCredenciales
{
    public class GetAllTiposCredencialesQuery: IGetAllTiposCredencialesQuery
    {
        private IDataBaseService _db;
        private IMapper _mapper;

        public GetAllTiposCredencialesQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }   

        public async Task<List<TipoCredencialModel>> Execute()
        {
            List<TipoCredencialModel> lista = await (from u in _db.TiposCredenciales
                                                     where u.ActivoDesde<= DateTime.Now && (u.ActivoHasta??DateTime.Now) >= DateTime.Now    
                                                     select new TipoCredencialModel
                                                {
                                                    Codigo = u.Codigo,
                                                    Nombre = u.Nombre,
                                                    VigenteDesde = u.ActivoDesde,
                                                    VigenteHasta = u.ActivoHasta
                                                }).ToListAsync<TipoCredencialModel>();


            return lista;
        }

    }
}
