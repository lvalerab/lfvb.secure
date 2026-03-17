using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class CodigoGestionTerritorialEntity
    {
        public Guid Id { get; set; }
        public string CodigoTipo { get; set; }
        public Guid IdElemento { get; set; }    
        public string Codigo { get; set; }

        public TipoCodigoGestionTerritorialEntity TipoCodigoGestionTerritorial { get; set; }
        public ElementoEntity Elemento { get; set; }    
    }
}
