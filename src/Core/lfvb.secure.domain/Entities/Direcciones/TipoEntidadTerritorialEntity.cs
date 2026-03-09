using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class TipoEntidadTerritorialEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public ICollection<EntitdadTerritorialEntity> Entidades { get; set; }   
    }
}
