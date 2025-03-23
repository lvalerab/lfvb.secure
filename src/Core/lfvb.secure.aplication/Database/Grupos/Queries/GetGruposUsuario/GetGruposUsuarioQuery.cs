using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario
{
    public class GetGruposUsuarioQuery:IGetGruposUsuario
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetGruposUsuarioQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<List<GetGruposUsuarioModel>?> Execute(Guid? id)
        {
            if (id != null)
            {
                Guid guid = id ?? Guid.NewGuid();
                List<GetGruposUsuarioModel> grupos = await (from us in _db.Usuarios
                                                            from gu in _db.Grupos
                                                            where gu.RelacionUsuarios.Any(p=>p.IdUsuario.Equals(us.Id))
                                                            && us.Id==guid
                                                            select new GetGruposUsuarioModel
                                                            {
                                                                Id = gu.Id,
                                                                UsuarioId = us.Id,
                                                                Nombre = gu.Nombre
                                                            }).ToListAsync<GetGruposUsuarioModel>();
                return grupos;
            } else
            {
                return null;
            }
        }

    }
}
