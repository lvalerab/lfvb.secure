using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.i18N
{
    public class CampoTextoEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid IdColeccion { get; set; } = Guid.Empty; 
        public string Nombre { get; set; } = "";
        

        public ColeccionTextoEntity Coleccion { get; set; }

        public IList<OpcionTextoEntity> Opciones { get; set; } = new List<OpcionTextoEntity>();
    }
}
