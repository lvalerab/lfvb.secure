using lfvb.secure.domain.Entities.Circuitos.Paso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Circuitos.BandejaTramite
{
    public class BandejaTramiteEntity
    {
        public Guid Id { get; set; }        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<PasoEntity> Pasos { get; set; } 
    }
}
