using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class DireccionNormalizadaModel
    {
        public CallejeroModel? Calle { get; set; }
        public string? Edificio { get;set;}
        public string? Numero { get; set; }
        public string? Puerta { get; set; } 
        public string? Piso { get;set; }
        public string? Escalera { get;set; }
        public string? Bloque { get;set; }
        public string? Ampliacion { get;set; }
    }
}
