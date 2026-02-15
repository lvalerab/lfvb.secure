using lfvb.secure.aplication.Database.i18N.Composiciones.Querys;
using lfvb.secure.aplication.Database.i18N.Idiomas.Queries;
using lfvb.secure.aplication.Database.i18N.Textos.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N
{
    public class DependencyI18NInjectionQueries
    {
        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            // Aquí puedes agregar las inyecciones de dependencias relacionadas con i18N

            #region "Idiomas"
            services.AddTransient<IGetAllIdiomasQuery, GetAllIdiomasQuery>();   
            services.AddTransient<IGetIdiomaQuery, GetIdiomaQuery>();
            services.AddTransient<IGetIdIdiomaQuery, GetIdIdiomaQuery>();

            #endregion

            #region "Colecciones de textos"
            services.AddTransient<IGetAllColeccionesTexto,GetAllColeccionesTexto>();
            services.AddTransient<IGetColeccionTextoQuery, GetColeccionTextoQuery>();
            services.AddTransient<IGetAllCamposColeccionTextoQuery, GetAllCamposColeccionTextoQuery>();
            services.AddTransient<IGetCampoColeccionTextoQuery, GetCampoColeccionTextoQuery>();
            services.AddTransient<IGetAllOpcionesCamposColeccionQuery,GetAllOpcionesCamposColeccionQuery>();
            services.AddTransient<IGetOpcionCampoColeccionTextoQuery, GetOpcionCampoColeccionTextoQuery>();
            #endregion

            #region "Textos"
            services.AddTransient<IGetAllTextos, GetAllTextos>();
            services.AddTransient<IBuscadorTextosQuery, BuscadorTextosQuery>();
            services.AddTransient<IGetTextoQuery, GetTextoQuery>();
            #endregion


            return services;
        }
    }
}
