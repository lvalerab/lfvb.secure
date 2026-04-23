using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class DireccionNoNormalizadaModel
    {
        public EntidadTerritorialModel? Entidad { get; set; }
        public CallejeroModel? Calle { get; set; }
        public string? Linea1 { get; set; }
        public string? Linea2 { get; set; }
        public string? Linea3 { get; set; }
    }
}
