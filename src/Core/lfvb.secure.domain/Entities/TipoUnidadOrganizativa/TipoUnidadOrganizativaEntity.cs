using lfvb.secure.domain.Entities.UnidadOrganizativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoUnidadOrganizativa
{
    public class TipoUnidadOrganizativaEntity
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }  
        public string Descripcion { get; set; } 


        public IList<UnidadOrganizativaEntity> UnidadesOrganizativas { get; set; }
    }
}
