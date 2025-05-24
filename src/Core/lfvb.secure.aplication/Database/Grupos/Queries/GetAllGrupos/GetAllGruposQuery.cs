using AutoMapper;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos
{
    public class GetAllGruposQuery:IGetAllGruposQuery
    {
        private IDataBaseService _db;
        private IMapper _mapper;    

        public GetAllGruposQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }   


        public async Task<List<GrupoModel>> Execute()
        {
            List<GrupoModel> grupos = await (from g in _db.Grupos
                                              select new GrupoModel
                                              {
                                                  Id = g.Id,
                                                  Nombre = g.Nombre
                                              }).ToListAsync<GrupoModel>();

            return grupos;
        }
    }
}
