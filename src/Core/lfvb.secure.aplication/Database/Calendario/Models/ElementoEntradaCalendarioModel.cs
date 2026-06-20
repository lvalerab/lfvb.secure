using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Models
{
    public class ElementoEntradaCalendarioModel
    {
        public Guid? IdEntradaCalendario { get; set; }  
        public Guid? IdElemento { get; set; }   
        public string Datos { get; set; }   
    }
}
