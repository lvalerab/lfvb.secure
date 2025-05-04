using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento
{
    public class ValorPropiedadModel
    {
        public Int64? Id {get;set;}
        public Int64? IdPropiedadElemento {get;set;}
        public String? Texto {get;set;}  
        public Double? Numero { get;set;}
        public DateTime? Fecha {get;set;}
        public Boolean? Bool {get;set;}
        public Double? NumeroMaximo {get;set;}
        public DateTime? FechaMaxima {get;set;}
    }
}
