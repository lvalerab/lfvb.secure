using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Commands.ActualizaUsuario
{
    public class ActualizaUsuarioCommand : IActualizaUsuarioCommand
    {
        private readonly IDataBaseService _bd;
        private readonly IMapper _mp;

        public ActualizaUsuarioCommand(IDataBaseService bd, IMapper mp)
        {
            _bd = bd ?? throw new ArgumentNullException(nameof(bd));
            _mp = mp ?? throw new ArgumentNullException(nameof(mp));
        }

        public Task<string> Validate(ActualizaUsuarioModel data)
        {
            string devolver = "";
            if(data == null)
            {
                devolver += "El usuario no puede ser nulo.";
            }
            if (string.IsNullOrEmpty(data.Usuario))
            {
                devolver += "El usuario no puede estar vacío.";
            }
            if (string.IsNullOrEmpty(data.Nombre))
            {
                devolver = "El nombre no puede estar vacío.";
            }
            if (string.IsNullOrEmpty(data.Email))
            {
                devolver = "El email no puede estar vacío.";
            }
            return Task.FromResult(devolver);
        }


        public Task<ActualizaUsuarioModel> Execute(ActualizaUsuarioModel data)
        {
            if(this.Validate(data).Result != "")
            {
                throw new ArgumentException(this.Validate(data).Result);
            } else
            {
                //Buscamos el usuario en la base de datos
                UsuarioEntity? usuario = _bd.Usuarios.FirstOrDefault(x => x.Id == data.Id);
                if(usuario == null)
                {
                    throw new KeyNotFoundException("El usuario no existe en la base de datos.");
                } else
                {
                    //Actualizamos los datos del usuario
                    //usuario.Usuario = data.Usuario;
                    usuario.Nombre = data.Nombre;
                    usuario.Apellido1 = data.Apellido1;
                    usuario.Apellido2 = data.Apellido2;
                    usuario.Email = data.Email;                   
                    //Guardamos los cambios en la base de datos
                    _bd.SaveAsync().Wait();
                    return Task.FromResult(data);
                }
            }
        }
    }
}
