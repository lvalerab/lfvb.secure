using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class RelacionPersonaModel
    {
        public Guid? Id { get; set; }
        public TipoRelacionPersonaModel Tipo { get; set; }
        public PersonaModel Persona1 { get; set; }
        public PersonaModel Persona2 { get; set; }
        public string Observaciones { get; set; }   
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; } 
    }
}
