using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Models
{
    public class FiltroEntradaCalendarioModel
    {
        public Guid IdCalendario { get; set; }
        public TipoEntradaCalendarioModel? TipoEntrada { get; set; } = null;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
