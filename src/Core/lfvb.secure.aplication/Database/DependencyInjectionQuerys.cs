using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using Microsoft.Extensions.DependencyInjection;


namespace lfvb.secure.aplication.Database
{
    public class DependencyInjectionQuerys
    {
        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            #region "Querys de usuario"
            services.AddTransient<IGetAllUsuariosQuery, GetAllUsuriosQuery>();
            services.AddTransient<ILoginUsuarioPasswordQuery, LoginUsuarioPasswordQuery>();
            services.AddTransient<ILoginTokenQuery, LoginTokenQuery>();
            #endregion

            #region "Querys de grupos"
            services.AddTransient<IGetGruposUsuario, GetGruposUsuarioQuery>();
            #endregion

            #region "Querys de aplicaciones"
            services.AddTransient<IGetAplicacionesUsuarioQuery, GetAplicacionesUsuarioQuery>();
            #endregion

            #region "Querys de tipos de propiedades"
            services.AddTransient<IGetAllTiposPropiedadesQuery, GetAllTiposPropiedadesQuery>();
            #endregion

            #region "Querys de propiedades"
            services.AddTransient<IGetAllPropiedadesQuery, GetAllPropiedadesQuery>();
            services.AddTransient<IGetPropiedadesElementoQuery, GetPropiedadElementoQuery>();
            #endregion
            return services;
        }
    }
}
