using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class PermisoElementoAplicacionQueryModel
    {
        public Guid? IdApli { get; set; }
        public Guid? IdElap { get; set; }
        public string Nombre { get; set; }
        public IList<string> CodigoTipoPermiso { get; set; }
    }
}
