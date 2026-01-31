using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.AgregarGrupoPermisosUsuario
{
    public class AgregarGrupoPermisosUsuarioCommand : IAgregarGrupoPermisosUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AgregarGrupoPermisosUsuarioCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<bool> Execute(Guid idUsuario, List<Guid> grupos)
        {
            List<Guid> ListaGruposUsuario = await (from g in _db.RelacionUsuariosGruposAplicaciones
                                                     where g.IdUsuario==idUsuario
                                                     select g.IdGrupo).ToListAsync();
            foreach(Guid grupo in grupos)
            {
                if (!ListaGruposUsuario.Contains(grupo))
                {
                    _db.RelacionUsuariosGruposAplicaciones.Add(new RelacionUsuarioGrupoUsuarioAplicacionEntity
                    {
                        IdUsuario = idUsuario,
                        IdGrupo = grupo
                    });
                }
            }
            await _db.SaveAsync();
            return true;
        }
    }
}
