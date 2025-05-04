using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades
{
    public class PropiedadModel
    {
        public string Codigo { get; set; }  
        public TipoPropiedadModel TipoPropiedad { get; set; }   
        public string Nombre { get; set; }
    }
}
