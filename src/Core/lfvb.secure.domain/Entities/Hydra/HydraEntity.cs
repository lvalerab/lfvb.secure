using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Hydra
{
    public class HydraEntity
    {
        public Guid Id { get; set; }    
        public String Nombre { get; set; }  
        public Guid? IdUsuaProp { get; set; }
        public Guid? IdUsuaEjec { get; set; }

        public UsuarioEntity? Propietario { get; set; }  
        public UsuarioEntity? Ejecutor { get; set; }


        public IList<LogHydraEntity> Logs { get; set; } = new List<LogHydraEntity>();
    }
}
