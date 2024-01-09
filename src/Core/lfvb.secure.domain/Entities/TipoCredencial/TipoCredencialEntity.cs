using lfvb.secure.domain.Entities.Credencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TipoCredencial
{
    public class TipoCredencialEntity
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public DateTime? ActivoDesde { get; set; }

        public DateTime? ActivoHasta { get; set; }

        public ICollection<CredencialEntity> Credenciales { get; set; } 
    }
}
