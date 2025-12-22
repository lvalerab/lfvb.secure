using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Queries
{
    public class ListaBandejasSistemaQuery: IListaBandejasSistemaQuery
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        public ListaBandejasSistemaQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Models.BandejaTramiteModel>> execute()
        {
            List<BandejaTramiteModel> bandejas = await (from b in _db.BandejasTramites
                                  select new BandejaTramiteModel
                                  {
                                      Id= b.Id,
                                      Nombre= b.Nombre,
                                      Descripcion= b.Descripcion
                                  }).ToListAsync();
            return bandejas;
        }
    }
}
