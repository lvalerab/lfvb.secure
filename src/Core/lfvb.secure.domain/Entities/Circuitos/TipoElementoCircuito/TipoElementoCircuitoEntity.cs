using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.TipoElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.TipoElementoCircuito
{
    public class TipoElementoCircuitoEntity
    {
        public string CodigoTipoElemento { get; set; }  
        public Guid IdCircuito { get; set; }

        public TipoElementoEntity TipoElemento { get; set; }
        public CircuitoEntity Circuito { get; set; }
    }
}
