using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario
{
    public interface IGetAplicacionesUsuarioQuery
    {
        public Task<List<GetAplicacionUsuarioModel>> Execute(Guid id);
    }
}
