using AutoMapper;
using lfvb.secure.aplication.Interfaces;
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

        public async Task<List<GetAllUsuariosModel>> Execute()
        {
            List<UsuarioEntity> lista = await this._db.Usuarios.ToListAsync();
            return this._mapper.Map<List<GetAllUsuariosModel>>(lista);
        }
    }
}
