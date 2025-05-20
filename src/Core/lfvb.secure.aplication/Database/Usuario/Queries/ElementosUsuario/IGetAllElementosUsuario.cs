using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.ElementosUsuario
{
    public interface IGetAllElementosUsuario
    {
        public Task<List<VMElementosModel>> Execute(Guid idUsuario);
        public Task<bool> Execute(Guid idUsuario, Guid idElemento);
    }
}
