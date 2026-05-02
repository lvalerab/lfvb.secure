using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class IdentificacionPersonaModel
    {
        public Guid? Id { get; set; }
        public TipoIdentificacionPersonaModel Tipo { get; set; }
        public PersonaModel? Persona { get; set; }
        public string Dato1 { get; set; }
        public string Dato2 { get; set; }
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; }
    }
}
