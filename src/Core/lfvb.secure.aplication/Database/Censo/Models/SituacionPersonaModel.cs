using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class SituacionPersonaModel
    {
        public Guid? Id { get; set; }
        public TipoSituacionPersonaModel Tipo { get; set; }
        public PersonaModel Persona { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }
}
