using lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario;
using lfvb.secure.aplication.Database.Calendario.Queries.GetEntradasCalendario;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario
{
    public class CalendarioDenpendencyInjection
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {

            return services;
        }

        public static IServiceCollection AddQueries(IServiceCollection services)
        {

            services.AddTransient<IGetCalendariosUsuarioQuery, GetCalendariosUsuarioQuery>();
            services.AddTransient<IGetEntradaCalendario, GetEntradaCalendario>();

            return services;
        }

        public static IServiceCollection AddCalendarioServices(IServiceCollection services)
        {
            AddCommands(services);
            AddQueries(services);

            return services;
        }
    }
}
