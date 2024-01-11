using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lfvb.secure.domain.Entities.ElementoAplicacion
{
    public class ElementoAplicacionEntity
    {

        public Guid Id { get; set; }

        public string CodigoTipoElemento { get; set; }

        public Guid IdAplicacion { get; set; }

        public string Nombre { get; set; }

        public Guid? IdPadre { get; set; }

        public List<ElementoAplicacionEntity> Descendientes { get; set; }

        public ElementoAplicacionEntity? Padre { get; set; }

        public TipoElementoAplicacion.TipoElementoAplicacionEntity TipoElementoAplicacion { get; set; }
    }
}
