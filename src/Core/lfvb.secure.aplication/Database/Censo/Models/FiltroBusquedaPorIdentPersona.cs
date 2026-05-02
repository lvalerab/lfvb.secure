using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class FiltroBusquedaPorIdentPersona
    {
        public TipoIdentificacionPersonaModel Tipo { get; set; }
        public string? Dato1 { get; set; }   
        public string? Dato2 { get; set; }
        public DateTime? FechaInicio { get; set; } = DateTime.MinValue;
        public DateTime? FechaFin { get; set; } =DateTime.MaxValue;
    }
}
