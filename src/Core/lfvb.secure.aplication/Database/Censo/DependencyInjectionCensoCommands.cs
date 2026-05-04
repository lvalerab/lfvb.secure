using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo
{
    public class DependencyInjectionCensoCommands
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddTransient<Commands.AltaPersona.IAltaPersonaCommand, Commands.AltaPersona.AltaPersonaCommand>();
            services.AddTransient<Commands.ModificaPersona.IModificarPersonaCommand, Commands.ModificaPersona.ModificarPersonaCommand>();

            services.AddTransient<Commands.AltaSituacionPersona.IAltaSituacionPersonaCommand, Commands.AltaSituacionPersona.AltaSituacionPersonaCommand>();
            services.AddTransient<Commands.ModificarSituacionPersona.IModificarSituacionPersonaCommand, Commands.ModificarSituacionPersona.ModificarSituacionPersonaCommand>();

            services.AddTransient<Commands.AltaRelacionPersona.IAltaModificacionRelacionPersonaCommand, Commands.AltaRelacionPersona.AltaModificacionRelacionPersonaCommand>();

            services.AddTransient<Commands.AgregarIdentificacion.IAltaModificacionIdentificacionPersonaCommand, Commands.AgregarIdentificacion.AltaModificacionIdentificacionPersonaCommand>();

            services.AddTransient<Commands.RelacionarElementoPersona.IRelacionarElementoPersonaCommand, Commands.RelacionarElementoPersona.RelacionarElementoPersonaCommand>();

            return services;
        }
    }
}
