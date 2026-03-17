using lfvb.secure.aplication.Database.Direcciones.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones
{
    public class DependencyDireccionesInjectionQueries
    {

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            // Aquí puedes agregar las inyecciones de dependencias relacionadas con direcciones
            #region "Entidades territoriales"
            services.AddTransient<IBuscarEntidadesQuery, BuscarEntidadesQuery>();
            services.AddTransient<IBuscadorCallejeroQuery, BuscadorCallejeroQuery>();
            services.AddTransient<IGetAllTiposEntidadesTerritorialesQuery, GetAllTiposEntidadesTerritorialesQuery>();
            services.AddTransient<IGetAllTiposViasQuery, GetAllTiposViasQuery>();
            services.AddTransient<IGetEntidadTerritorialQuery,GetEntidadTerritorialQuery>();
            #endregion


            return services;
        }   
    }
}
