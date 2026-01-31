using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaPermisoElementoAplicacion
{
    public class AltaPermisoElementoAplicacionCommand:IAltaPermisoElementoAplicacionCommand
    {
        private readonly IDataBaseService _db;
        public AltaPermisoElementoAplicacionCommand(IDataBaseService db)
        {
            this._db = db;
        }

        public async Task<AltaPermisoElementoAplicacionModel> Execute(AltaPermisoElementoAplicacionModel permiso)
        {
            RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity nuevo= new RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity
            {
                IdGrupo = permiso.IdGrupo,
                IdElemento = permiso.IdElementoAplicacion,
                CodigoTipoPermiso = permiso.CodigoTipoPermiso
            };
            _db.RelacionElementosConTiposPermisosConGruposUsuarios.Add(nuevo);
            await _db.SaveAsync();
            return permiso;
        }
    }
}
