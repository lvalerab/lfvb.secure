using lfvb.secure.domain.Entities.PropiedadElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Elemento
{
    public class ElementoEntity
    {
        public Guid Id { get; set; }
        
        public ICollection<PropiedadElementoEntity>? Propiedades { get; set; }
    }
}
