using lfvb.secure.domain.Entities.PropiedadElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.ValorPropiedadElemento
{
    public class ValorPropiedadElementoEntity
    {
        public Int32 Id { get; set; }
        public Int32 IdPropiedadElemento { get; set; }
        public PropiedadElementoEntity PropiedadElemento { get; set; }
        public String? Texto { get; set; }
        public Double? Numerico { get; set; }
        public String? Booleano { get; set; }
        public DateTime? Fecha { get; set; }
        public Double? NumericoMaximo { get; set; } 
        public DateTime? FechaMaximo { get; set; }
    }
}
