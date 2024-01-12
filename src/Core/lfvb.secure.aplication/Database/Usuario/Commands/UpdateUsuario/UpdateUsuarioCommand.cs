using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommand : IUpdateUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public UpdateUsuarioCommand(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<UpdateUsuarioModel> Execute(UpdateUsuarioModel model)
        {
            UsuarioEntity entidad=this._mapper.Map<UsuarioEntity>(model);
            this._db.Usuarios.Update(entidad);
            await this._db.SaveAsync();
            return model;
        }
    }
}
