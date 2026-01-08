using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.UnidadOrganizativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.UnidadOrganizativaElemento
{
    public class UnidadOrganizativaElementoEntity
    {
        public Guid CodUnor { get; set; }   
        public Guid IdElem { get; set; }    

        public UnidadOrganizativaEntity UnidadOrganizativa { get; set; }
        public ElementoEntity Elemento { get; set; }
    }
}
