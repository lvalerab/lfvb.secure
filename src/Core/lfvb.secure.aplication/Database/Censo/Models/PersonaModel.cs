using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class PersonaModel
    {
        public Guid? Id { get; set; }
        public TipoPersonaModel Tipo { get; set; }
        public string Nombre { get; set; }  
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public List<IdentificacionPersonaModel> Identificaciones { get; set; }
        public List<SituacionPersonaModel> Situaciones { get; set; }
        public List<RelacionPersonaModel> Relaciones { get; set; }
    }
}
