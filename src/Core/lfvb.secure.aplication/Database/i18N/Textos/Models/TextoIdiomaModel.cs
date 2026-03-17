using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Models
{
    public class TextoIdiomaModel
    {
        public Guid? Id { get; set; }   
        public IdiomaModel Idioma { get; set; }
        public String Texto { get; set; }   
    }
}
