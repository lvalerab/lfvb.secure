using lfvb.secure.domain.Entities.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.PropiedadValoresSql
{
    public class PropiedadValoresSqlEntity
    {
        public string Codigo { get; set; }  
        public string Etiqueta { get; set; }    
        public string FiltrarPorId { get; set; }  
        public string Sql { get; set; } 

        public PropiedadEntity Propiedad { get; set; }
    }
}
