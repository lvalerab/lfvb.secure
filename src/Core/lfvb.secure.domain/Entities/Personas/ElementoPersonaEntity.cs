using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class ElementoPersonaEntity
    {
        public Guid IdPersona { get; set; } 
        public Guid IdElemento { get; set; }

        public PersonaEntity Persona { get; set; }
        public ElementoEntity Elemento { get; set; }
    }
}
