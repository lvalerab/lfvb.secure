using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Models
{
    public class TextoModel
    {
        public Guid? Id { get; set; }
        public List<VariableTextoModel> Variables { get; set; }
        public List<TextoIdiomaModel> Textos { get; set; }
        public TextoColumnaIdiomaModel? Columnas { get; set; }   
    }
}
