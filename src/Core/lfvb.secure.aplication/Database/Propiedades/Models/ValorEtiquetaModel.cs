using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Models
{

    public class ValorEtiquetaModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class GrupoValorEtiquetaModel
    {
            public string Label { get; set; }   
            public string Value { get; set; }   
            public List<ValorEtiquetaModel> Items { get; set; }
    }
}
