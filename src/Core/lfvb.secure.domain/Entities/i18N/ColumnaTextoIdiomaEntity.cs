using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class ColumnaTextoIdiomaEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string CodIdioma { get; set; } = ""; 
        public string CodIdiomaRelacionado { get; set; } = "";
        public string Contenido { get; set; } = "";

        public TextoEntity Texto { get; set; }
        public AgrupacionIdiomaEntity AgrupacionIdioma { get; set; }
        
    }
}
