using lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaActualizacionElementoAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaAplicacion;
using lfvb.secure.aplication.Database.Credencial.Commands.CaducarCredencial;
using lfvb.secure.aplication.Database.Credencial.Commands.CrearCredencialUsuario;
using lfvb.secure.aplication.Database.Grupos.Commands.ActualizaGrupoUsuariosPerisos;
using lfvb.secure.aplication.Database.Grupos.Commands.AltaGrupoUsuariosPermisos;
using lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento;
using lfvb.secure.aplication.Database.Usuario.Commands.ActualizaUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.AgregarGrupoPermisosUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.QuitarGrupoPermisosUsuario;
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
            services.AddTransient<IAgregarGrupoPermisosUsuarioCommand, AgregarGrupoPermisosUsuarioCommand>();
            services.AddTransient<IQuitarGrupoPermisosUsuario, QuitarGrupoPermisosUsuario>();
            services.AddTransient<IActualizaUsuarioCommand, ActualizaUsuarioCommand>();
            #endregion

            #region "Relativos a grupos"
            services.AddTransient<IAltaGrupoUsuariosPermisosCommand, AltaGrupoUsuariosPermisosCommand>();   
            services.AddTransient<IActualizaGrupoUsuariosPermisosCommand, ActualizaGrupoUsuariosPermisosCommand>();
            #endregion

            #region "Relativos a credenciales"
            services.AddTransient<ICrearCredencialUsuarioCommand, CrearCredencialUsuarioCommand>();
            services.AddTransient<ICaducarCredencialCommand, CaducarCredencialCommand>();
            #endregion

            #region "Relativos a propiedades"
            services.AddTransient<INuevaActualizaPropiedadElementoCommand, NuevaActualizaPropiedadElementoCommand>();
            #endregion

            #region "Relativos a aplicaciones"
            services.AddTransient<IAltaAplicacionCommand, AltaAplicacionCommand>();
            services.AddTransient<IAltaActualizacionElementoAplicacionCommand, AltaActualizacionElementoAplicacionCommand>();   
            #endregion

            return services;
        }
    }
}
