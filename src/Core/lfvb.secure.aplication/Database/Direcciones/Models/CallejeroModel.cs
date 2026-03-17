using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class CallejeroModel
    {
        public Guid? Id { get; set; }   
        public EntidadTerritorialModel EntidadTerritorial { get; set; } 
        public TipoViaModel TipoVia { get; set; }   
        public string Nombre { get; set; }

        public CallejeroModel? CalleSuperior { get; set; }
        public List<CallejeroModel>? CallesInferiores { get; set; }
    }
}
