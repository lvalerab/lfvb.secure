using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisoElementoAplicacion
{
    public interface IPermisoElementoAplicacionQuery
    {
        public Task<PermisoElementoAplicacionQueryModel> Execute(Guid idUsuario, Guid idAplicacion, Guid idElementoAplicacion, string? CodigoTipoPermiso=null);
        public Task<PermisoElementoAplicacionQueryModel> Execute(Guid idUsuario, string codAplicacion, string codElementoAplicacion, string? CodigoTipoPermiso = null);
        public PermisoElementoAplicacionQueryModel ExecuteSync(Guid idUsuario, Guid idAplicacion, Guid idElementoAplicacion, string? CodigoTipoPermiso = null);
        public PermisoElementoAplicacionQueryModel ExecuteSync(Guid idUsuario, string codAplicacion, string codElementoAplicacion, string? CodigoTipoPermiso = null);
    }
}
