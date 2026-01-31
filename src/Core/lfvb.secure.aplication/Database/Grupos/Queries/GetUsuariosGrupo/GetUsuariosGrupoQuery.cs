using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetUsuariosGrupo
{
    public class GetUsuariosGrupoQuery:IGetUsuariosGrupoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetUsuariosGrupoQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<UsuarioModel>> Execute(Guid grupoId)
        {
            List<UsuarioModel> usuarios = await (from u in _db.Usuarios.Include(x=>x.RelacionGrupos)
                                                    where u.RelacionGrupos.Any(g => g.IdGrupo == grupoId)
                                                    select new UsuarioModel
                                                    {
                                                        Id = u.Id,
                                                        Nombre = u.Nombre,
                                                        Apellido1 = u.Apellido1,
                                                        Apellido2 = u.Apellido2,
                                                        Email = u.Email,
                                                        Usuario = u.Usuario
                                                    }).ToListAsync<UsuarioModel>();

            return usuarios;
        }
    }
}
