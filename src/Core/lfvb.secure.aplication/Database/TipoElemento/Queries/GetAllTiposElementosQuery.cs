using AutoMapper;
using lfvb.secure.aplication.Database.TipoElemento.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoElemento.Queries
{
    internal class GetAllTiposElementosQuery : IGetAllTiposElementosQuery
    {
        private IDataBaseService _db;
        private IMapper _mapper;

        public GetAllTiposElementosQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TipoElementoModel>> Execute()
        {
            List<TipoElementoModel> result = await (from tp in _db.TiposElementos
                                                   select new TipoElementoModel
                                                   {
                                                       Codigo = tp.Codigo,
                                                       Nombre = tp.Nombre
                                                   }).ToListAsync();
            return result;
        }
    }
}
