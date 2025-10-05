using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class AltaPermisoElementoAplicacionModel
    {
        public Guid IdGrupo { get; set; }
        public Guid IdElementoAplicacion { get; set; }
        public string CodigoTipoPermiso { get; set; } = string.Empty;
    }
}
