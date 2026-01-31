using lfvb.secure.domain.Entities.Circuitos.Paso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.PasoSiguiente
{
    public class PasoSiguienteEntity
    {
        public Guid IdPaso { get; set; }    
        public Guid IdPasoSiguiente { get; set; }   

        public PasoEntity Paso { get; set; }
        public PasoEntity PasoSiguiente { get; set; }
    }
}
