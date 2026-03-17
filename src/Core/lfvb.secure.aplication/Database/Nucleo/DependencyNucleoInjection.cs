
using lfvb.secure.aplication.Database.Nucleo.Querys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Nucleo
{
    public class DependencyNucleoInjection
    {
        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            // Aquí puedes agregar las inyecciones de dependencias relacionadas con i18N
            services.AddTransient<IGetIdentificadorNucleoSistemaQuery, GetIdentificadorNucleoSistemaQuery>();

            return services;
        }
    }
}
