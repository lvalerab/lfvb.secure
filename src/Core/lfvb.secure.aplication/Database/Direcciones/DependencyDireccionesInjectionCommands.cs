using lfvb.secure.aplication.Database.Direcciones.Commands.Callejero;
using lfvb.secure.aplication.Database.Direcciones.Commands.Direccion;
using lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoCodigoEntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoEntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoVia;
using lfvb.secure.aplication.Database.Direcciones.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones
{
    public class DependencyDireccionesInjectionCommands
    {

        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            // Aquí puedes agregar las inyecciones de dependencias relacionadas con direcciones
            #region "Maestros"
            services.AddTransient<IAltaTipoViaCommand, AltaTipoViaCommand>();
            services.AddTransient<IAltaTipoEntidadTerritorialCommand,AltaTipoEntidadTerritorialCommand>();
            services.AddTransient<IAltaTipoCodigoEntidadTerritorialCommand, AltaTipoCodigoEntidadTerritorialCommand>();
            #endregion

            #region "Entidades territoriales"
            services.AddTransient<IAltaEntidadTerritorialCommand, AltaEntidadTerritorialCommand>(); 
            services.AddTransient<IModificarEntidadTerritorialCommand, ModificarEntidadTerritorialCommand>();
            services.AddTransient<IEliminarEntidadTerritorialCommand, EliminarEntidadTerritorialCommand>();
            #endregion

            #region "Callejero"
            services.AddTransient<IAltaViaCommand, AltaViaCommand>();
            services.AddTransient<IModificarViaCommand, ModificarViaCommand>();
            services.AddTransient<IEliminarViaCommand, EliminarViaCommand>();
            #endregion

            #region "Direcciones"
            services.AddTransient<IAltaModificacionDireccionCommand, AltaModificacionDireccionCommand>();
            #endregion


            return services;
        }
    }
}
