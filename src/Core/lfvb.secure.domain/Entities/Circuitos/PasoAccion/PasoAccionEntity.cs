using lfvb.secure.domain.Entities.Circuitos.Accion;
using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.PasoAccion
{
    public class PasoAccionEntity
    {
        public Int64 Id { get; set; }   
        public Guid IdPaso { get; set; }
        public String TipoEjecucion { get; set; }
        public int Orden { get; set; }    
        public Guid IdAccion { get; set; }
        public Guid? IdCircuitoError { get; set; }


        public PasoEntity Paso { get; set; }
        public AccionEntity Accion { get; set; }
        public CircuitoEntity CircuitoError { get; set; }
    }
}
