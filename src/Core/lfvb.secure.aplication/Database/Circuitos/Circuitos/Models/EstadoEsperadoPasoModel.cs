using lfvb.secure.aplication.Database.TipoElemento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Models
{
    public class EstadoEsperadoPasoModel
    {
        public PasoModel Paso { get; set; }
        public TipoElementoModel TipoElemento { get; set; } 
        public EstadoModel Estado { get; set; }
        public string TipoEstadoEsperado { get; set; }  
    }
}
