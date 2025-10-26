using lfvb.secure.aplication.Database.Circuitos.Tramites.Commands;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos
{
    public  class CircuitosDependencyInjection
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddTransient<IAltaTramiteCommand, AltaTramiteCommand>();
            services.AddTransient<IModificarTramiteCommand, ModificarTramiteCommand>(); 
            return services;
        }

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {

            services.AddTransient<IGetAllTramitesQuery, GetAllTramitesQuery>();
            services.AddTransient<IGetTramiteQuery, GetTramiteQuery>();

            return services;
        }   
    }
}
