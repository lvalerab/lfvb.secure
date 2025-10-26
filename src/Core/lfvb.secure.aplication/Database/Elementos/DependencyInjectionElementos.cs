using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite;
using lfvb.secure.aplication.Database.Elementos.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Elementos
{
    public class DependencyInjectionElementos
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddTransient<IAltaElementoCommand,AltaElementoCommand>();

            return services;
        }

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {

            return services;
        }
    }
}
