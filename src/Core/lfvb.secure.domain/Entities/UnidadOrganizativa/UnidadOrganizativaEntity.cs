using lfvb.secure.domain.Entities.GrupoUnidadOrganizativa;
using lfvb.secure.domain.Entities.TipoUnidadOrganizativa;
using lfvb.secure.domain.Entities.UnidadOrganizativaElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.UnidadOrganizativa
{
    public class UnidadOrganizativaEntity
    {
        public Guid Codigo { get; set; }
        public Guid CodTuno { get; set; }
        public Guid? CodUnorPadre { get; set; } 
        public string Nombre { get; set; }  
        public string Descripcion { get; set; }


        public TipoUnidadOrganizativaEntity TipoUnidadOrganizativa { get; set; }
        public UnidadOrganizativaEntity? UnidadOrganizativaPadre { get; set; }

        public IList<UnidadOrganizativaEntity> UnidadesOrganizativasHijas { get; set; }
        public IList<GrupoUnidadOrganizativaEntity> GruposUnidadesOrganizativas { get; set; }
        public IList<GrupoUnidadOrganizativaEntity> GruposUnidadesOrganizativasRelacionadas { get; set; }
        public IList<UnidadOrganizativaElementoEntity> Elementos { get; set; }
    }
}
