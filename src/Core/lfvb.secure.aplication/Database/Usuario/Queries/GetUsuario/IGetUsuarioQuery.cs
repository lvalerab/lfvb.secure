using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.GetUsuario
{
    public interface IGetUsuarioQuery
    {
        public Task<UsuarioModel> Execute(Guid? id);
    }
}
