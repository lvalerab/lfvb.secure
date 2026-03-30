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
        private List<string>? _permisos;

        public DbAuthorizeAttribute(string app) : base()
        {
            
            // Constructor que recibe solo el nombre de la política y la aplicación
            this._permisos = null;
            this._app = app;
            this._componente = null;
            this._permiso = null;
        }
        
        public DbAuthorizeAttribute(string app, string componente) : base()
        {
            this._permisos = null;  
            _app = app;
            _componente = componente;
            _permiso = null;
        }

        public DbAuthorizeAttribute(string app, string componente, string permiso) : base()
        {
            this._permisos = null;
            _app = app;
            _componente = componente;
           _permiso = permiso;
        }

        public DbAuthorizeAttribute(string[] permisos):base()
        {
            _permisos = permisos.ToList();
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
            }
            else
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(httpContext);
                if (id == null)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    if (this._permisos != null)
                    {
                        foreach(string perm in this._permisos)
                        {
                            string[] parts = perm.Split(':');
                            if(parts.Length ==3)
                            {
                                this._app = parts[0];  
                                this._componente = parts[1];
                                this._permiso = parts[2];
                            } else if(parts.Length==2)
                            {
                                this._app = parts[0];
                                this._componente = parts[1];
                            } else if(parts.Length==1)
                            {
                                this._app = parts[0];
                            }
                            PermisoElementoAplicacionQueryModel authorized = this._permisoElementoAplicacionQuery.ExecuteSync(id ?? Guid.Empty, _app, _componente, _permiso);
                            if (authorized.CodigoTipoPermiso.Count > 0)
                            {
                                //Si encuentra el permiso, se autoriza y se sale del ciclo, si no encuentra el permiso, se devuelve un 401  
                                return;
                            }
                        }
                        //En caso de no encontrar ningún permiso, se devuelve un 401    
                        context.Result = new UnauthorizedResult();
                    }
                    else
                    {
                        PermisoElementoAplicacionQueryModel authorized = this._permisoElementoAplicacionQuery.ExecuteSync(id ?? Guid.Empty, _app, _componente, _permiso);
                        if (!(authorized.CodigoTipoPermiso.Count > 0))
                        {
                            context.Result = new UnauthorizedResult();
                        }
                    }
                }
            }            
        }
    }
}
