using lfvb.secure.aplication.Database.i18N.Composiciones.Commands;
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

            #region "Relativos a las colecciones de texto"
            services.AddTransient<IAltaColeccionTextoCommand, AltaColeccionTextoCommand>();
            services.AddTransient<IModificaColeccionTexto, ModificaColeccionTexto>();
            services.AddTransient<IEliminarColeccionTextoCommand,EliminarColeccionTextoCommand>();

            services.AddTransient<IAltaCampoColeccionTextoCommand, AltaCampoColeccionTextoCommand>();
            services.AddTransient<IModificarCampoColeccionTextoCommand, ModificarCampoColeccionTextoCommand>(); 
            services.AddTransient<IEliminarCamposColeccionTextoCommand, EliminarCamposColeccionTextoCommand>();   

            services.AddTransient<IAltaOpcionCampoColeccionTextoCommand, AltaOpcionCampoColeccionTextoCommand>();
            services.AddTransient<IModificaOpcionCampoColeccionTextoCommand, ModificaOpcionCampoColeccionTextoCommand>();
            services.AddTransient<IEliminarOpcionCamposColeccionTextosCommand,EliminarOpcionCamposColeccionTextosCommand>();   
            #endregion


            return services;

        }
    }
}
