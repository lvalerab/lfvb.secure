using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoCrendecial.Models
{
    public class TipoCredencialModel
    {
        public String? Codigo { get; set; }
        public String? Nombre { get; set; }
        public DateTime? VigenteDesde { get; set; }
        public DateTime? VigenteHasta { get; set; }
    }
}
