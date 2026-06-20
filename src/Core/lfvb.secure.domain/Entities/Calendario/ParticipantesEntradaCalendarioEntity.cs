using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Calendario
{
    public class ParticipantesEntradaCalendarioEntity
    {
        public Guid Id { get; set; }
        public Guid IdEntradaCalendario { get; set; }
        public Guid IdElem { get; set; }
        public string EMail { get; set; }

        public EntradaCalendarioEntity EntradaCalendario { get; set; }
    }
}
