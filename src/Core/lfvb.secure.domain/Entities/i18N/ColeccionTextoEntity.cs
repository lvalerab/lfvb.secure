using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class ColeccionTextoEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Nombre { get; set; } = "";    
        public string Descripcion { get; set; } = "";


        public IList<CampoTextoEntity> Campos { get; set; } = new List<CampoTextoEntity>();
    }
}
