using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Extensions.Pagination;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites
{
    public class GetAllTramitesQuery : IGetAllTramitesQuery
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetAllTramitesQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<TramiteModel>> Execute()
        {
            List<TramiteModel> tramites = await (from tr in _db.Tramites
                                         select new TramiteModel
                                         {
                                             Id = tr.Id,
                                             Nombre = tr.Nombre
                                         }).ToListAsync<TramiteModel>(); 

            return tramites;
        }

        public async Task<List<TramiteModel>> Execute(int page, int registros)
        {
            List<TramiteModel> tramites = await (from tr in _db.Tramites

                                                 select new TramiteModel
                                                 {
                                                     Id = tr.Id,
                                                     Nombre = tr.Nombre
                                                 }).ToListAsync<TramiteModel>();

            tramites=tramites.Page<TramiteModel>(page, registros).ToList();

            return tramites;
        }
    }
}
