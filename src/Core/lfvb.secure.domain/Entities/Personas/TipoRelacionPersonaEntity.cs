using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class TipoRelacionPersonaEntity
    {
        public Guid? Id { get; set; }   
        public string Codigo { get; set; }  
        public string Nombre { get; set; } 
        public string? CodigoReciproco { get; set; } 

        public ICollection<RelacionPersonaEntity> Relaciones { get; set; }
        public TipoRelacionPersonaEntity? TipoReciploco { get; set; }
        public List<TipoRelacionPersonaEntity> TiposReciprocos { get; set; }
    }
}
