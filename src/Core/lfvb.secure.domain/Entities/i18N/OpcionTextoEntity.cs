using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class OpcionTextoEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid IdCampo { get; set; } = Guid.Empty; 
        public Guid IdTexto { get; set; } = Guid.Empty;
        public string Opcion { get; set; } = "";
        
        public CampoTextoEntity Campo { get; set; }
        public TextoEntity Texto { get; set; }  
    }
}
