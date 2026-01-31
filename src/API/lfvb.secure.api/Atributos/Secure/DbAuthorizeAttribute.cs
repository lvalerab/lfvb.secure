using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisoElementoAplicacion;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lfvb.secure.api.Atributos.Secure
{
    public class DbAuthorizeAttribute:ActionFilterAttribute
    {
        private IPermisoElementoAplicacionQuery _permisoElementoAplicacionQuery;
        private IJwtTokenUtils _jwtTokenUtils;
        private string? _app;
        private string? _componente;
        private string? _permiso;

        public DbAuthorizeAttribute(string app) : base()
        {
            
            // Constructor que recibe solo el nombre de la política y la aplicación
            this._app = app;
            this._componente = null;
            this._permiso = null;
        }
        
        public DbAuthorizeAttribute(string app, string componente) : base()
        {
            _app = app;
            _componente = componente;
            _permiso = null;
        }

        public DbAuthorizeAttribute(string app, string componente, string permiso) : base()
        {
            _app = app;
            _componente = componente;
           _permiso = permiso;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            this._permisoElementoAplicacionQuery = httpContext.RequestServices.GetService<IPermisoElementoAplicacionQuery>();
            this._jwtTokenUtils = httpContext.RequestServices.GetService<IJwtTokenUtils>();
            // Aquí puedes agregar lógica personalizada para la autorización
            // Por ejemplo, verificar si el usuario tiene acceso a la base de datos
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.StatusCode = 401; // No autorizado
                return;
            } else
            {
                Guid? id= this._jwtTokenUtils.GetIdFromToken(httpContext);
                if(id==null)
                {
                    context.Result = new UnauthorizedResult();
                } else {
                    PermisoElementoAplicacionQueryModel authorized = this._permisoElementoAplicacionQuery.ExecuteSync(id ?? Guid.Empty, _app, _componente, _permiso);
                    
                    if (!(authorized.CodigoTipoPermiso.Count>0))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
