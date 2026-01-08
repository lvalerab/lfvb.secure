using lfvb.secure.domain.Entities.Circuitos.Estado;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.TipoElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.EstadoEsperadoPaso
{
    public class EstadoEsperadoPasoEntity
    {
        public Guid IdPaso { get; set; }  
        public string? CodTipoElemento { get; set; }    
        public string CodEstado { get; set; }   
        public string TipoEstadoEsperado { get; set; }  


        public PasoEntity Paso { get; set; }    
        public TipoElementoEntity? TipoElemento { get; set; }
        public EstadoEntity Estado { get; set; }
    }
}
