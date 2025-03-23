using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario
{
    public class GetAplicacionesUsuarioQuery: IGetAplicacionesUsuarioQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetAplicacionesUsuarioQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }


        public async Task<List<GetAplicacionUsuarioModel>> Execute(Guid id)
        {
            List<GetAplicacionUsuarioModel> aplicaciones = await (from ap in _db.Aplicaciones
                                                                  where ap.Grupos.Any(p => p.RelacionUsuarios.Any(z => z.IdUsuario == id))
                                                                  select new GetAplicacionUsuarioModel
                                                                  {
                                                                      Id = ap.Id,
                                                                      UserId = id,
                                                                      Nombre = ap.Nombre
                                                                  }).ToListAsync<GetAplicacionUsuarioModel>();
            return aplicaciones;
        }
    }
}
