using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisoElementoAplicacion
{
    public class PermisoElementoAplicacionQuery : IPermisoElementoAplicacionQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public PermisoElementoAplicacionQuery(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }


        public async Task<PermisoElementoAplicacionQueryModel> Execute(Guid idUsuario, Guid idAplicacion, Guid idElementoAplicacion, string? CodigoTipoPermiso = null)
        {
            PermisoElementoAplicacionQueryModel permiso = new PermisoElementoAplicacionQueryModel
            {
                IdApli = idAplicacion,
                IdElap = idElementoAplicacion,
                Nombre = string.Empty,
                CodigoTipoPermiso = new List<string>()
            };

            permiso.CodigoTipoPermiso = await (from u in _db.Usuarios
                                                join rgu in _db.RelacionUsuariosGruposAplicaciones on u.Id equals rgu.IdUsuario
                                                join g in _db.Grupos on rgu.IdGrupo equals g.Id
                                                join ap in _db.Aplicaciones on g.IdAplicacion equals ap.Id
                                                join ea in _db.ElementosAplicaciones on ap.Id equals ea.IdAplicacion
                                                join rga in _db.RelacionElementosConTiposPermisosConGruposUsuarios on ea.Id equals rga.IdElemento
                                                where g.Id.Equals(rga.IdGrupo)
                                                && u.Id.Equals(idUsuario)
                                                && ap.Id.Equals(idAplicacion)
                                                && ea.Id.Equals(idElementoAplicacion)
                                                && (CodigoTipoPermiso == null || rga.CodigoTipoPermiso.Equals(CodigoTipoPermiso))
                                                select rga.CodigoTipoPermiso
                                                ).ToListAsync<string>();
            return permiso;
        }

        public async Task<PermisoElementoAplicacionQueryModel> Execute(Guid idUsuario, string codAplicacion, string codElementoAplicacion, string? CodigoTipoPermiso = null)
        {
            PermisoElementoAplicacionQueryModel permiso = new PermisoElementoAplicacionQueryModel
            {
                IdApli = null,
                IdElap = null,
                Nombre = codAplicacion+" - "+codElementoAplicacion+" - "+CodigoTipoPermiso,
                CodigoTipoPermiso = new List<string>()
            };

            permiso.CodigoTipoPermiso = await (from u in _db.Usuarios
                                               join rgu in _db.RelacionUsuariosGruposAplicaciones on u.Id equals rgu.IdUsuario
                                               join g in _db.Grupos on rgu.IdGrupo equals g.Id
                                               join ap in _db.Aplicaciones on g.IdAplicacion equals ap.Id
                                               join ea in _db.ElementosAplicaciones on ap.Id equals ea.IdAplicacion
                                               join rga in _db.RelacionElementosConTiposPermisosConGruposUsuarios on ea.Id equals rga.IdElemento
                                               where g.Id.Equals(rga.IdGrupo)
                                               && u.Id.Equals(idUsuario)
                                               && ap.Codigo.Equals(codAplicacion)
                                               && ea.Codigo.Equals(codElementoAplicacion)
                                               && (CodigoTipoPermiso == null || rga.CodigoTipoPermiso.Equals(CodigoTipoPermiso))
                                               select rga.CodigoTipoPermiso
                                                ).ToListAsync<string>();
            return permiso;
        }

        public PermisoElementoAplicacionQueryModel ExecuteSync(Guid idUsuario, Guid idAplicacion, Guid idElementoAplicacion, string? CodigoTipoPermiso = null)
        {
            var tsk=Task.Run(async ()=> await this.Execute(idUsuario, idAplicacion, idElementoAplicacion, CodigoTipoPermiso));
            return tsk.Result;
        }

        public PermisoElementoAplicacionQueryModel ExecuteSync(Guid idUsuario, string codAplicacion, string codElementoAplicacion, string? CodigoTipoPermiso = null)
        {
            var tsk = Task.Run(async () => await this.Execute(idUsuario, codAplicacion, codElementoAplicacion, CodigoTipoPermiso));
            return tsk.Result;
        }
    }
}
