using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public  class DireccionEntity
    {
        public Guid Id { get; set; }    
        
        public DireccionNormalizadaEntity? DireccionNormalizada { get; set; }
        public DireccionNoNormalizadaEntity? DireccionNoNormalizada { get; set; } 

    }
}
