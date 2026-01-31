using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento
{
    public interface INuevaActualizaPropiedadElementoCommand
    {
        Task<PropiedadElementoModel> Execute(PropiedadElementoModel propiedad);
    }
}
