using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAllAplicaciones;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetGruposAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisoElementoAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisosElementosAplicacionPorGrupoYAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.TiposPermisosElementoPorTipoQuery;
using lfvb.secure.aplication.Database.Circuitos;
using lfvb.secure.aplication.Database.Elementos;
using lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGrupo;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Grupos.Queries.GetUsuariosGrupo;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetValoresSqlPropiedad;
using lfvb.secure.aplication.Database.TipoCrendecial.Queries.GetAllTiposCredenciales;
using lfvb.secure.aplication.Database.TipoElemento.Queries;
using lfvb.secure.aplication.Database.TipoElementoAplicacion.Queries.GetAllTiposElementosAplicacion;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.aplication.Database.UnidadesOrganizativas;
using lfvb.secure.aplication.Database.Usuario.Queries.ElementosUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Database.Usuario.Queries.GetCredencialesUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.GetUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using Microsoft.Extensions.DependencyInjection;


namespace lfvb.secure.aplication.Database
{
    public class DependencyInjectionQuerys
    {
        public static IServiceCollection AddQuerys(IServiceCollection services)
        {

            DependencyInjectionElementos.AddQuerys(services);

            #region "Querys de usuario"
            services.AddTransient<IGetAllUsuariosQuery, GetAllUsuriosQuery>();
            services.AddTransient<ILoginUsuarioPasswordQuery, LoginUsuarioPasswordQuery>();
            services.AddTransient<ILoginTokenQuery, LoginTokenQuery>();
            services.AddTransient<IGetCredencialesUsuarioQuery, GetCredencialesUsuarioQuery>();
            services.AddTransient<IGetUsuarioQuery, GetUsuarioQuery>(); 
            #endregion

            #region "Querys de grupos"
            services.AddTransient<IGetGruposUsuario, GetGruposUsuarioQuery>();
            services.AddTransient<IGetAllGruposQuery, GetAllGruposQuery>(); 
            services.AddTransient<IGetUsuariosGrupoQuery, GetUsuariosGrupoQuery>();
            services.AddTransient<IGetGrupoQuery, GetGrupoQuery>(); 
            #endregion

            #region "Querys de aplicaciones"
            services.AddTransient<IGetAplicacionesUsuarioQuery, GetAplicacionesUsuarioQuery>();
            services.AddTransient<IPermisoElementoAplicacionQuery, PermisoElementoAplicacionQuery>();
            services.AddTransient<IGetAllAplicacionesQuery, GetAllAplicacionesQuery>();
            services.AddTransient<IGetAplicacionQuery, GetAplicacionQuery>();
            services.AddTransient<IGetArbolElementosAplicacion, GetArbolElementosAplicacion>();
            services.AddTransient<IGetGruposAplicacionQuery, GetGruposAplicacionQuery>();
            #endregion

            #region "elementos de aplicacion"
            services.AddTransient<IGetAllTiposElementosAplicacionQuery, GetAllTiposElementosAplicacionQuery>();
            #endregion

            #region "Permisos de elementos de aplicacion"
            services.AddTransient<IPermisosElementosAplicacionPorGrupoYAplicacionQuery, PermisosElementosAplicacionPorGrupoYAplicacionQuery>();
            services.AddTransient<ITiposPermisosElementoPorTipoQuery, TiposPermisosElementoPorTipoQuery>();
            #endregion

            #region "Querys de tipos de propiedades"
            services.AddTransient<IGetAllTiposPropiedadesQuery, GetAllTiposPropiedadesQuery>();
            #endregion

            #region "Querys de propiedades"
            services.AddTransient<IGetAllPropiedadesQuery, GetAllPropiedadesQuery>();
            services.AddTransient<IGetPropiedadesElementoQuery, GetPropiedadElementoQuery>();
            services.AddTransient<IGetValoresSqlPropiedadQuery, GetValoresSqlPropiedadQuery>(); 
            #endregion

            #region "Querys de vistas"
            services.AddTransient<IGetAllElementosUsuario, GetAllElementosUsuario>();
            #endregion

            #region "Querys generales de core"
            services.AddTransient<IGetAllTiposElementosQuery, GetAllTiposElementosQuery>(); 
            #endregion

            #region "Credenciales"
            services.AddTransient<IGetAllTiposCredencialesQuery, GetAllTiposCredencialesQuery>();
            #endregion

            CircuitosDependencyInjection.AddQuerys(services);   

            UnidadesOrganizativasDependencyInjection.AddQuerys(services);


            return services;
        }
    }
}
