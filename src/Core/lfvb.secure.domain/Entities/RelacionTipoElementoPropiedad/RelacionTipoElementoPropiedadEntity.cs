using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.TipoElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.RelacionTipoElementoPropiedad
{
    public class RelacionTipoElementoPropiedadEntity
    {
        public string CodigoTipoElemento { get; set; }
        public string CodigoPropiedad { get; set; } 

        public TipoElementoEntity TipoElemento { get; set; }
        public PropiedadEntity Propiedad { get; set; }  
    }
}
