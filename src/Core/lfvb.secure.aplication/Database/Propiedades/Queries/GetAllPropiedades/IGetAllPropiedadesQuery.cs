using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades
{
    public interface IGetAllPropiedadesQuery
    {
        public Task<List<PropiedadModel>> Execute(string? CodPropiedadPadre, string? CodTipoElemento = null);    
    }
}
