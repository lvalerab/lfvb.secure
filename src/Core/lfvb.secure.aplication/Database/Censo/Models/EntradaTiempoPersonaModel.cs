using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class EntradaTiempoPersonaModel
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }  
        public SituacionPersonaModel Situacion { get; set; }
        public RelacionPersonaModel Relacion { get; set; }
    }
}
