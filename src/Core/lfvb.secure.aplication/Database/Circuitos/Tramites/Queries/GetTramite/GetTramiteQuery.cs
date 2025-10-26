using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite
{
    public class GetTramiteQuery: IGetTramiteQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetTramiteQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;   
        }

        public async Task<TramiteModel> Execute(Guid id)
        {
            TramiteModel tramite = await (from tr in _db.Tramites
                                          where tr.Id == id
                                          select new TramiteModel
                                          {
                                              Id = tr.Id,
                                              Nombre = tr.Nombre,
                                              Descripcion = tr.Descripcion,
                                              Normativa = tr.Normativa
                                          }
                                         ).FirstOrDefaultAsync();

            return tramite;
        }
    }
}
