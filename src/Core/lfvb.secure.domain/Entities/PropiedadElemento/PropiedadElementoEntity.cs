using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.PropiedadElemento
{
    public class PropiedadElementoEntity
    {
        public Int32 Id { get; set; }
        public Guid? IdElemento { get; set; }   
        public ElementoEntity? Elemento { get; set; }
        public String CodPropiedad { get; set; }
        public PropiedadEntity Propiedad { get; set; }
        public DateTime? FechaValor {get;set;}
        public String? Activo { get; set; } 

        public ICollection<ValorPropiedadElementoEntity> Valores { get; set; }
    }
}
