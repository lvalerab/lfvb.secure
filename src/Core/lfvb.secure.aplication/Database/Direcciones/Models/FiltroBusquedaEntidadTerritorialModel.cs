using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class FiltroBusquedaEntidadTerritorialModel
    {
        public List<TipoEntidadTerritorialModel>? TiposEntidades { get; set; }
        public List<EntidadTerritorialModel>? Padres { get; set; } 
        public string? Nombre { get; set; }  
    }
}
