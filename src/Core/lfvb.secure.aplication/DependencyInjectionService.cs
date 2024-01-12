using AutoMapper;
using lfvb.secure.aplication.Configurations;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Commands.UpdateUsuario;
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
            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();
            services.AddTransient<IUpdateUsuarioCommand, UpdateUsuarioCommand>();

            return services;
        }
    }
}
