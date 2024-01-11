using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Configurations
{
    public class MapperProfile:Profile
    {
        public MapperProfile() {
            //Esto es para mapear objetos

            //CreateMap<UsuarioEntity, CreateUsuarioModel>().ReverseMap();
        }
    }
}
