using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class DireccionNormalizadaEntity
    {
        public Guid Id { get; set; }        
        public Guid IdCalle { get; set; }
        public string? Edificio { get; set; }    
        public string? Numero { get; set; }
        public string? Puerta { get; set; }
        public string? Piso { get; set; }
        public string? Escalera { get; set; }
        public string? Bloque { get; set; }
        public string? Ampliacion { get; set; } 


        public DireccionEntity Direccion { get; set; }
        public CallejeroEntity Callejero { get; set; }
    }
}
