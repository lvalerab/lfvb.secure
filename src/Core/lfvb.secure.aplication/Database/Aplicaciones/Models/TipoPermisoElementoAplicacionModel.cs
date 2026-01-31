using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class TipoPermisoElementoAplicacionModel
    {
        public TipoElementoAplicacionModel TipoElemento { get; set; } = new TipoElementoAplicacionModel();
        public string Codigo { get;set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;  
    }
}
