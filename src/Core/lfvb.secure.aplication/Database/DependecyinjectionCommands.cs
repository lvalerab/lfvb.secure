using lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database
{
    public class DependecyinjectionCommands
    {
        public static IServiceCollection AddCommands(IServiceCollection services)
        {

            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();
            services.AddTransient<IUpdateUsuarioCommand, UpdateUsuarioCommand>();

            services.AddTransient<INuevaActualizaPropiedadElementoCommand, NuevaActualizaPropiedadElementoCommand>();
            return services;
        }
    }
}
