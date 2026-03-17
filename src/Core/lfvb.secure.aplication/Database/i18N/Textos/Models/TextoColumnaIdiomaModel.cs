using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Models
{
    public class TextoColumnaIdiomaModel
    {
        public Guid? Id { get; set; }
        public TextoIdiomaModel Columna1 { get; set; }  
        public TextoIdiomaModel Columna2 { get; set; }  
    }
}
