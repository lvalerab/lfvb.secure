using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Models
{
    public class ParticipanteEntradaCalendarioModel
    {
        public Guid? Id { get; set; }
        public Guid? IdEntradaCalendario { get; set; }
        public Guid? IdElemento { get; set; }
        public string Mail { get; set; }    
    }
}
