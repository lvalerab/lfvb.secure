using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword
{
    public interface ILoginUsuarioPasswordQuery
    {
        public Task<LoginUsuarioPasswordModel> Execute(LoginUsuarioPasswordModel parameters);
    }
}
