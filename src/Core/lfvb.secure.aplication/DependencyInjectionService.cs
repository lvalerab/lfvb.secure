using lfvb.secure.aplication.Configurations;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
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
            AutoMapper.MapperConfiguration mapper = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper);

            //Registramos los commands y los querys
            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();

            return services;
        }
    }
}
