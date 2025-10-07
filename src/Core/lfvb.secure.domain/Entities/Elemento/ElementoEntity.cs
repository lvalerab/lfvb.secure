using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.TipoElemento;
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

        public string CodigoTipoElemento { get; set; }  

        public TipoElementoEntity TipoElemento { get; set; }
        
        public ICollection<PropiedadElementoEntity>? Propiedades { get; set; }

        public ICollection<EstadoElementoEntity> Estados { get; set; }
    }
}
