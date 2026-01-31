using lfvb.secure.domain.Entities.UnidadOrganizativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.GrupoUnidadOrganizativa
{
    public class GrupoUnidadOrganizativaEntity
    {
        public Guid CodUnor { get; set; }
        public Guid CodUnorRelacionado { get; set; }


        public UnidadOrganizativaEntity UnidadOrganizativa { get; set; }
        public UnidadOrganizativaEntity UnidadOrganizativaRelacionado { get; set; }
    }
}
