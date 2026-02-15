using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Models
{
    public class BusquedaTextosModel
    {
        public string Busqueda { get; set; }
        public bool MatchExacto { get; set; } = false;
        public List<string>? Idiomas = null;
    }
}
