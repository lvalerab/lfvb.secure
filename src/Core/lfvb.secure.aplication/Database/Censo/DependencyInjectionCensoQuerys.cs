using lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas;
using lfvb.secure.aplication.Database.Censo.Queries.GetIdentificadores;
using lfvb.secure.aplication.Database.Censo.Queries.GetPersona;
using lfvb.secure.aplication.Database.Censo.Queries.GetRelaciones;
using lfvb.secure.aplication.Database.Censo.Queries.GetSituaciones;
using lfvb.secure.aplication.Database.Censo.Queries.Maestros;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo
{
    public class DependencyInjectionCensoQuerys
    {

        public static IServiceCollection AddQuerys(IServiceCollection services)
        {
            
            services.AddTransient<IGetPersonaQuery, GetPersonaQuery>();
            services.AddTransient<IBuscadorPersonaQuery, BuscadorPersonaQuery>();

            services.AddTransient<IGetAllTiposIdentificadoresPersonaQuery, GetAllTiposIdentificadoresPersonaQuery>();
            services.AddTransient<IGetAllTipoSituacionPersonaModel, GetAllTipoSituacionPersonaModel>();
            services.AddTransient<IGetAllTiposPersonaQuery, GetAllTiposPersonaQuery>();
            services.AddTransient<IGetTipoRelacionPersonaQuery, GetTipoRelacionPersonaQuery>(); 
            services.AddTransient<IGetAllTipoSexoPersonaQuery, GetAllTipoSexoPersonaQuery>(); 
            
            services.AddTransient<IGetIdentificadoresPersonaQuery, GetIdentificadoresPersonaQuery>();
            services.AddTransient<IGetRelacionesPersonaQuery, GetRelacionesPersonaQuery>();
            services.AddTransient<IGetSituacionesPersonalesQuery, GetSituacionesPersonalesQuery>();

            return services;
        }
    }
}
