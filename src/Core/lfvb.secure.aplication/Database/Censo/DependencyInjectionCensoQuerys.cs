using lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas;
using lfvb.secure.aplication.Database.Censo.Queries.GetPersona;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo
{
    public class DependencyInjectionCensoQuerys
    {

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            
            services.AddTransient<IGetPersonaQuery, GetPersonaQuery>();
            services.AddTransient<IBuscadorPersonaQuery, BuscadorPersonaQuery>();

            return services;
        }
    }
}
