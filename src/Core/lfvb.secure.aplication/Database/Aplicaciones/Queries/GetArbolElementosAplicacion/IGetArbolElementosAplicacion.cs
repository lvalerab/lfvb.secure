using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion
{
    public interface IGetArbolElementosAplicacion
    {
        public Task<List<ElementoAplicacionModel>> Execute(Guid idAplicacion, Guid? idElementoPadre = null);
    }
}
