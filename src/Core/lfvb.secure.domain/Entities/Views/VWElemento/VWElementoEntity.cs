using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Views.VWElemento
{
    public class VWElementoEntity
    {
        public Guid? Id { get; set; }
        public string? Etiqueta { get; set; }   
        public string? Tipo { get; set; }   
        public Guid? IdUsuario { get; set; }    
    }
}
