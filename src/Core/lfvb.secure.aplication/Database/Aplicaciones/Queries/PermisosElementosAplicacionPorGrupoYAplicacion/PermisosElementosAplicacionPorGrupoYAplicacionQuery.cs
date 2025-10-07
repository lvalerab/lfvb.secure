using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisosElementosAplicacionPorGrupoYAplicacion
{
    public class PermisosElementosAplicacionPorGrupoYAplicacionQuery : IPermisosElementosAplicacionPorGrupoYAplicacionQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public PermisosElementosAplicacionPorGrupoYAplicacionQuery(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }

        public async Task<List<PermisoElementoAplicacionModel>> Execute(Guid idAplicacion, Guid idGrupo)
        {
            List<PermisoElementoAplicacionModel> permisos = await (from pr in _db.RelacionElementosConTiposPermisosConGruposUsuarios.Include(p => p.TipoPermiso)
                                                                                                                                    .Include(p => p.ElementoAplicacion)
                                                                                                                                    .Include(p => p.Grupo)
                                                                   where pr.IdGrupo.Equals(idGrupo) && pr.ElementoAplicacion.IdAplicacion.Equals(idAplicacion)
                                                                   select new PermisoElementoAplicacionModel
                                                                   {
                                                                       Grupo = new GrupoModel
                                                                       {
                                                                           Id = pr.IdGrupo,
                                                                           Nombre = pr.Grupo.Nombre
                                                                       },
                                                                       Elemento = new ElementoAplicacionModel
                                                                       {
                                                                           Id = pr.IdElemento,
                                                                           Nombre = pr.ElementoAplicacion.Nombre,
                                                                           Codigo = pr.ElementoAplicacion.Codigo,
                                                                           Aplicacion = new AplicacionModel
                                                                           {
                                                                               Id = pr.ElementoAplicacion.IdAplicacion,
                                                                               Nombre = pr.ElementoAplicacion.Aplicacion.Nombre,
                                                                               Codigo = pr.ElementoAplicacion.Aplicacion.Codigo
                                                                           },
                                                                           TipoElemento = new TipoElementoAplicacionModel
                                                                           {
                                                                               Codigo = pr.ElementoAplicacion.CodigoTipoElemento,
                                                                               Nombre = pr.ElementoAplicacion.TipoElementoAplicacion.Nombre,
                                                                           }
                                                                       },
                                                                       TipoPermiso = new TipoPermisoElementoAplicacionModel
                                                                       {
                                                                           Codigo = pr.CodigoTipoPermiso,
                                                                           Nombre = pr.TipoPermiso.Nombre
                                                                       }
                                                                   }).ToListAsync<PermisoElementoAplicacionModel>();

            return permisos;
        }
    }
}
