using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class TipoIdentificadorPersonaEntity
    {
        public Guid? Id { get; set; }
        public string Codigo { get; set; }  
        public string Nombre { get; set; }  
        
        public ICollection<IdentificadorPersonaEntity> Identificadores { get; set; }    
    }
}
