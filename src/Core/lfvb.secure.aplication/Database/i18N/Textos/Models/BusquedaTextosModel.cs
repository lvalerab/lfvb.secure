using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
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
        public bool MatchExacto { get; set; }
        public List<String?> Idiomas { get; set; }
    }
}
