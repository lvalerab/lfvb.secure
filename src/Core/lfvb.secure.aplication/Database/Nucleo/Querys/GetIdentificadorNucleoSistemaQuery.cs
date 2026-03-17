using AutoMapper;
using lfvb.secure.aplication.Database.Nucleo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Nucleo.Querys
{
    public class GetIdentificadorNucleoSistemaQuery: IGetIdentificadorNucleoSistemaQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetIdentificadorNucleoSistemaQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<NucleoSistemaModel> execute()
        {
            NucleoSistemaModel nucleo=await (from nc in _db.NucleosSistemas
                                            select new NucleoSistemaModel
                                            {
                                                Id = nc.IdNucleo
                                            }).FirstOrDefaultAsync();
            return nucleo;
        }
    }
}
