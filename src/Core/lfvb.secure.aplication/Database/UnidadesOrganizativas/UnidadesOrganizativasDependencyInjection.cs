using lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Tipos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas
{
    public class UnidadesOrganizativasDependencyInjection
    {
        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            services.AddTransient<Queries.Tipos.IGetAllTiposUnidadesOrganizativasQuery, Queries.Tipos.GetAllTiposUnidadesOrganizativasQuery>();
            services.AddTransient<Queries.Unidades.IGetArbolUnidadesOrganizativasQuery, Queries.Unidades.GetArbolUnidadesOrganizativasQuery>(); 
            return services;
        }

        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddTransient<IAltaTipoUnidadOrganizativaCommand, AltaTipoUnidadOrganizativaCommand>();
            services.AddTransient<IModificarTipoUnidadOrganizativaCommand, ModificarTipoUnidadOrganizativaCommand>();

            return services;
        }
    }
}
