using lfvb.secure.aplication.Database.Credencial.Commands.CaducarCredencial;
using lfvb.secure.aplication.Database.Credencial.Commands.CrearCredencialUsuario;
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
            #region "Relativos a usuarios"
            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();
            services.AddTransient<IUpdateUsuarioCommand, UpdateUsuarioCommand>();
            #endregion

            #region "Relativos a credenciales"
            services.AddTransient<ICrearCredencialUsuarioCommand, CrearCredencialUsuarioCommand>();
            services.AddTransient<ICaducarCredencialCommand, CaducarCredencialCommand>();
            #endregion

            services.AddTransient<INuevaActualizaPropiedadElementoCommand, NuevaActualizaPropiedadElementoCommand>();
            return services;
        }
    }
}
