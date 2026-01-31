using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class VariableTextoEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid IdTexto { get; set; } = Guid.Empty; 
        public string Variable { get; set; } = "";

        public TextoEntity Texto { get; set; }  
    }
}
