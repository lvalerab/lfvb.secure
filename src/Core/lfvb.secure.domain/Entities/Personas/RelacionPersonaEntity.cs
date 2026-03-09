using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class RelacionPersonaEntity
    {
        public Guid IdPersona { get; set; }
        public Guid IdPersonaRelacionada { get; set; }
        public string CodigoTipoRelacion { get; set; }
        public DateTime InicioVigencia { get; set; }    
        public DateTime? FinVigencia { get; set; }
        public string Observaciones { get; set; }   

        public PersonaEntity Persona { get; set; }  
        public PersonaEntity PersonaRelacionada { get; set; }  
        public TipoRelacionPersonaEntity TipoRelacionPersona { get; set; }
    }
}
