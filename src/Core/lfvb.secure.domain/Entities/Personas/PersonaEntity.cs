using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class PersonaEntity
    {
        public Guid Id { get; set; }
        public string CodigoTipoPersona { get; set; }   
        public string Nombre { get; set; }  
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string CodigoSexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public TipoPersonaEntity TipoPersona { get; set; } 
        public TipoSexoPersonaEntity TipoSexo { get; set; }

        public ICollection<ElementoPersonaEntity> Elementos { get; set; }
        public ICollection<IdentificadorPersonaEntity> Identificadores { get; set; }
        public ICollection<RelacionPersonaEntity> Relaciones { get; set; }  // de esta persona hacia otras
        public ICollection<RelacionPersonaEntity> Relacionados { get; set; } // de otras personas hacia esta
        public ICollection<SituacionPersonaEntity> Situaciones { get; set; }    

    }
}
