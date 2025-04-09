using AutoMapper;
using lfvb.secure.aplication.Configurations;
using lfvb.secure.aplication.Database;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.common.PASSWORD;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddAplication(this IServiceCollection services,IConfiguration configuration)
        {
            /////
            ///Configuracion del automapper
            /////
            MapperConfiguration mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());

            //Registramos los commands y los querys
            DependecyinjectionCommands.AddCommands(services);
            DependencyInjectionQuerys.AddQuerys(services);

            //Configuramos el servicio de DataProtection
            services.AddDataProtection();

            //Registramos los utiles de datos
            services.AddTransient<ISecurePassword, SecurePassword>();

            return services;
        }
    }
}
