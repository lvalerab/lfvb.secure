using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.NucleoSistema
{
    public class NucleoSistemaEntity
    {
        public string Codigo { get; set; } = "core";
        public Guid IdNucleo { get; set; } = Guid.Empty;

        public ElementoEntity Elemento { get; set; } = new ElementoEntity();
    }
}
