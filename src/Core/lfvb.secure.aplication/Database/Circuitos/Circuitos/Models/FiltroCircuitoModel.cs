using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Models
{
    public class FiltroCircuitoModel
    {
        public Guid? IdTramite { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool? Activo { get; set; } = null;
        public int? page { get; set; } = 0;
        public int? regs { get; set; } = 10;    
    }
}
