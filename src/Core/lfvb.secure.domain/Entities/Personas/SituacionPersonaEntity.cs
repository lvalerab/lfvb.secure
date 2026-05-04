using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Personas
{
    public class SituacionPersonaEntity
    {
        public Guid? Id { get; set; }   
        public Guid? IdPersona { get; set; }    
        public string CodigoSituacion { get; set; } 
        public DateTime FechaDesde { get; set; }= DateTime.Now;
        public DateTime? FechaHasta { get; set; } = null;
        public string Observaciones { get; set; }   

        public PersonaEntity Persona { get; set; }  
        public TipoSituacionPersonaEntity TipoSituacionPersona { get; set; }    
    }
}
