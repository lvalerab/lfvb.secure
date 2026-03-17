using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class IdentificadorPersonaEntity
    {
        public Guid Id { get; set; }
        public Guid IdPersona { get; set; }
        public string CodigoTipoIdentificador { get; set; }  
        public string Dato1 { get; set; }  
        public string Dato2 { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FinVigencia { get; set; }

        public PersonaEntity Persona { get; set; }
        public TipoIdentificadorPersonaEntity TipoIdentificadorPersona { get; set; }    
    }
}
