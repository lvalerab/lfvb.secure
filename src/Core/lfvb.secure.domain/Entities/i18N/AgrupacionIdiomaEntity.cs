using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class AgrupacionIdiomaEntity
    {
        public string Codigo { get; set; } = "";    
        public string CodigoIdiomaRelacionado { get; set; } = "";   
        public int Orden { get; set; } = 0; 

        public IdiomaEntity Idioma { get; set; }
        public IdiomaEntity IdiomaRelacionado { get; set; }

        public IList<ColumnaTextoIdiomaEntity> ColumnasTexto { get; set; } = new List<ColumnaTextoIdiomaEntity>();
    }
}
