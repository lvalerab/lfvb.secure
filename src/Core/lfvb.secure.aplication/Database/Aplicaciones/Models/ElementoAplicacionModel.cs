using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class ElementoAplicacionModel
    {
        public Guid? Id { get; set; }
        public ElementoAplicacionModel Padre { get; set; }
        public AplicacionModel Aplicacion { get; set; }
        public TipoElementoAplicacionModel TipoElemento { get; set; }
        public string? Nombre { get; set; }
        public List<ElementoAplicacionModel> Elementos { get; set; } = new List<ElementoAplicacionModel>();
    }
}
