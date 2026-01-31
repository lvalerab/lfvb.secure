using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Models
{
    public class IdiomaModel
    {
        public string Codigo { get; set; }  
        public string Nombre { get; set; }  
        public bool Multiple { get; set; }
        public int? Orden { get; set; } = null;
        public List<PropiedadElementoModel> Propiedades { get; set; } = new List<PropiedadElementoModel>();
        public List<IdiomaModel> Componentes { get; set; } = new List<IdiomaModel>();
    }
}
