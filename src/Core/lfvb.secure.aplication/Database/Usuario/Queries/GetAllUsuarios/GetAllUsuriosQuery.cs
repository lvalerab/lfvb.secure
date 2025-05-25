using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios
{
    public class GetAllUsuriosQuery: IGetAllUsuariosQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetAllUsuriosQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<List<UsuarioModel>> Execute(int? pagina=null, int? elementos=null)
        {
            List<UsuarioModel> lista = await (from u in _db.Usuarios                                               
                                               select new UsuarioModel
                                               {
                                                   Id=u.Id,
                                                   Usuario=u.Usuario,
                                                   Nombre=u.Nombre,
                                                   Apellido1=u.Apellido1,
                                                   Apellido2=u.Apellido2
                                               } ).ToListAsync<UsuarioModel>();
            return lista;
        }
    }
}
