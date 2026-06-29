using lfvb.secure.aplication.Database.Calendario.Commands.AltaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.AltaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.DesrelacionaEntradaCalendarioUsuario;
using lfvb.secure.aplication.Database.Calendario.Commands.EliminaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.EliminarCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.ModificaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.ModificarCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.RelacionaEntradaCalendarioUsuario;
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

            services.AddTransient<IAltaCalendarioUsuarioCommand, AltaCalendarioUsuarioCommand>();
            services.AddTransient<IModificarCalendarioCommand, ModificarCalendarioCommand>();
            services.AddTransient<IEliminarCalendarioCommand, EliminarCalendarioCommand>();

            services.AddTransient<IAltaEntradaCalendarioUsuarioCommand, AltaEntradaCalendarioUsuarioCommand>();
            services.AddTransient<IModificarEntradaCalendarioCommand, ModificarEntradaCalendarioCommand>();
            services.AddTransient<IEliminarEntradaCalendarioCommand, EliminarEntradaCalendarioCommand>();

            services.AddTransient<IRelacionarEntradaCalendarioUsuarioCommand, RelacionarEntradaCalendarioUsuarioCommand>();
            services.AddTransient<IDesrelacionarEntradaCalendarioUsuarioCommand, DesrelacionarEntradaCalendarioUsuarioCommand>();

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
