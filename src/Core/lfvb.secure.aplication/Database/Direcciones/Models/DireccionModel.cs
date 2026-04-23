using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Models
{
    public class DireccionModel
    {
        public Guid? Id { get; set; }

        public DireccionNormalizadaModel? Normalizada { get; set; }
        public DireccionNoNormalizadaModel? NoNormalizada { get; set; }
    }
}
