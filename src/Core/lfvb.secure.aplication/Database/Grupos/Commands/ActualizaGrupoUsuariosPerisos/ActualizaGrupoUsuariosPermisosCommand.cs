using AutoMapper;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Commands.ActualizaGrupoUsuariosPerisos
{
    public class ActualizaGrupoUsuariosPermisosCommand : IActualizaGrupoUsuariosPermisosCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public ActualizaGrupoUsuariosPermisosCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mapper = mp;
        }

        public async Task<GrupoModel> Execute(GrupoModel grupo)
        {
            GrupoUsuariosAplicacionEntity entity= await (from g in _db.Grupos
                                                        where g.Id == grupo.Id
                                                        select g).FirstOrDefaultAsync<GrupoUsuariosAplicacionEntity>(); 
            if(entity == null)
            {
                throw new Exception("No se ha encontrado el grupo de permisos indicado");
            } else
            {
                entity.Nombre = grupo.Nombre;
                _db.Grupos.Update(entity);    
                await _db.SaveAsync(); //Guardamos los cambios en el grupo
            }
            return grupo;
        }
    }
}
