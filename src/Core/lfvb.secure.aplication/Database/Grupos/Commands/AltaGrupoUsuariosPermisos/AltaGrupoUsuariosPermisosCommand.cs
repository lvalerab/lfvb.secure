using AutoMapper;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Commands.AltaGrupoUsuariosPermisos
{
    public class AltaGrupoUsuariosPermisosCommand : IAltaGrupoUsuariosPermisosCommand
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AltaGrupoUsuariosPermisosCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mapper = mp;
        }   

        public async Task<GrupoModel> Execute(GrupoModel grupo)
        {
            GrupoUsuariosAplicacionEntity entity = null;  
            entity=new GrupoUsuariosAplicacionEntity
            {
                IdAplicacion=grupo.Aplicacion?.Id??Guid.Empty,
                Nombre = grupo.Nombre,
                RelacionUsuarios = new List<RelacionUsuarioGrupoUsuarioAplicacionEntity>()
            };
            if(grupo.Usuarios!=null && grupo.Usuarios.Count > 0)
            {
                foreach (var usuario in grupo.Usuarios)
                {
                    RelacionUsuarioGrupoUsuarioAplicacionEntity relacion = new RelacionUsuarioGrupoUsuarioAplicacionEntity
                    {
                        IdUsuario = usuario.Id??Guid.Empty,
                        Grupo = entity
                    };
                    entity.RelacionUsuarios.Add(relacion);
                }
            }
            _db.Grupos.Add(entity);
            await _db.SaveAsync(); 
            grupo.Id = entity.Id;
            return grupo;
        }
    }
}
