using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Acciones.Models
{
    public class AccionModel
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public string? Nombre { get; set; } = null;        
    }
}
