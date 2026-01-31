using lfvb.secure.aplication.Database.i18N.Idiomas.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N
{
    public class DependencyI18NInjectionCommands
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            // Aquí puedes agregar las inyecciones de dependencias relacionadas con i18N
            services.AddTransient<IAltaIdiomaCommand, AltaIdiomaCommand>();
            services.AddTransient<IModificarIdiomaCommand, ModificarIdiomaCommand>();


            return services;

        }
    }
}
