using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Models
{
    public class VariableTextoModel
    {
        public Guid? Id { get; set; }        
        public TextoModel Texto { get; set; }
        public String Variable { get; set; }   
    }
}
