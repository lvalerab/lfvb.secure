using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoPropiedad.Queries
{
    public interface IGetAllTiposPropiedadesQuery
    {
        public Task<List<TipoPropiedadModel>> Execute();
    }
}
