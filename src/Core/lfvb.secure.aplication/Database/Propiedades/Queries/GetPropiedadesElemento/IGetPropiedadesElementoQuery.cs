using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento
{
    public interface IGetPropiedadesElementoQuery
    {
        public Task<List<PropiedadElementoModel>> Execute(Guid idElemento);
        public Task<List<PropiedadElementoModel>> Execute(List<Guid?> idElementos);
        public Task<List<PropiedadElementoModel>> Execute(List<Guid?> idElementos, List<string> CodigoPropiedad);
    }
}
