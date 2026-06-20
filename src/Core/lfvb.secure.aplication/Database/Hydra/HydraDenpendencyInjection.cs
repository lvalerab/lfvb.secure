using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Hydra
{
    public class HydraDenpendencyInjection
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddQueries(IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddHydraServices(IServiceCollection services)
        {
            AddCommands(services);
            AddQueries(services);
            return services;
        }
    }
}
