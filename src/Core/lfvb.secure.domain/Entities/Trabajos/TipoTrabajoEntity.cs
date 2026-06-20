using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Trabajos
{
    public class TipoTrabajoEntity
    {
        public Guid Id { get; set; }  
        public string Codigo { get; set; } = string.Empty;  
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty; 

        public ICollection<TrabajoEntity> Trabajos { get; set; } = new List<TrabajoEntity>();   
    }
}
