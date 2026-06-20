using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Hydra
{
    public class LogHydraEntity
    {
        public Int64 Id { get; set; }
        public Guid IdHydra { get; set; }
        public DateTime Fecha { get; set; }=DateTime.Now;
        public String Tipo { get; set; } = "I";
        public String Mensaje { get; set; } 
        public String Datos { get; set; }= String.Empty;

        public HydraEntity Hydra { get; set; } 
    }
}
