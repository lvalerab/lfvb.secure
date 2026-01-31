using lfvb.secure.aplication.Database.i18N.Idiomas.Queries;
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

            services.AddTransient<IGetAllIdiomasQuery, GetAllIdiomasQuery>();   
            services.AddTransient<IGetIdiomaQuery, GetIdiomaQuery>();

            return services;
        }
    }
}
