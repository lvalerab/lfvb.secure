using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class AltaModificacionRelacionPersonalModel
    {
        public TipoRelacionPersonaModel? Tipo { get; set; }
        public Guid? IdPersona1 { get; set; }
        public Guid? IdPersona2 { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; } = DateTime.MaxValue;
        public string? Observaciones { get; set; }
    }
}
