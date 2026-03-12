using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class EntidadTerritorialModel
    {
        public Guid? Id { get; set; }
        public EntidadTerritorialModel? Padre { get; set; }
        public TipoEntidadTerritorialModel? Tipo { get; set; }
        public string? Nombre { get; set; }
    }
}
