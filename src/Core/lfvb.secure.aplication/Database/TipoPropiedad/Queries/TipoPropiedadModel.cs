using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoPropiedad.Queries
{
    public class TipoPropiedadModel
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Multiple { get; set; }
        public bool Historico { get; set; } 
        public bool Intervalo { get; set; }
        public string Tipo { get; set; }
        public bool ListaValores { get; set; }
    }
}
