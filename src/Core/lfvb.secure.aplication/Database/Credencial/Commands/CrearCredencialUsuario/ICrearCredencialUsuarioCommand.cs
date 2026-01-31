using lfvb.secure.aplication.Database.Credencial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Commands.CrearCredencialUsuario
{
    public interface ICrearCredencialUsuarioCommand
    {
        public Task<CredencialModel> execute(CredencialModel credencialModel);
    }
}
