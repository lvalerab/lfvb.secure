using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Acciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Acciones.Queries
{
    public class GetAllAccionesQuery : IGetAllAccionesQuery
    {
        
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetAllAccionesQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<AccionModel>> execute()
        {
            return await (from a in _db.Acciones
                          select new AccionModel
                          {
                              Id = a.Id,
                              Nombre = a.Nombre
                          }).ToListAsync();
        }
    }
}
