using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class FiltroBusquedaCallejeroModel
    {
        public List<EntidadTerritorialModel>? EntidadesTerritoriales { get; set; }
        public List<TipoViaModel>? TiposVia { get; set; }   
        public string? Nombre { get; set; }

        public List<CallejeroModel>? CallesSuperiores { get; set; }
        public List<CallejeroModel>? CallesInferiores { get; set; }     
    }
}
