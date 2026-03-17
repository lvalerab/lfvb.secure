using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class DireccionNoNormalizadaEntity
    {
        public Guid Id { get; set; }
        public Guid? IdCalle { get; set; }
        public Guid? IdEntidadTerritorial { get; set; }
        public string Linea1 { get; set; }
        public string Linea2 { get; set; }
        public string Linea3 { get; set; }
        
        public DireccionEntity Direccion { get; set; }
        public CallejeroEntity? Callejero { get; set; }
        public EntitdadTerritorialEntity? EntidadTerritorial { get; set; }
    }
}
