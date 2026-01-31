using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
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
            List<GrupoModel> grupos = await (from g in _db.Grupos.Include(p=>p.Aplicacion)
                                             select new GrupoModel
                                              {
                                                  Id = g.Id,
                                                  Nombre = g.Nombre,
                                                  Aplicacion=new AplicacionModel
                                                  {
                                                      Id = g.Aplicacion.Id,
                                                      Codigo = g.Aplicacion.Codigo,
                                                      Nombre = g.Aplicacion.Nombre
                                                  }
                                              }).ToListAsync<GrupoModel>();

            return grupos;
        }
    }
}
