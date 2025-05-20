using lfvb.secure.domain.Entities.Aplicacion;
using lfvb.secure.domain.Entities.RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacion;
using lfvb.secure.domain.Entities.RelacionTipoElementoApliConTipoPermisoTipoElementoApli;
using lfvb.secure.domain.Entities.TipoElementoAplicacion;
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

        public string Codigo { get; set; }

        public string CodigoTipoElemento { get; set; }

        public Guid IdAplicacion { get; set; }

        public string Nombre { get; set; }

        public Guid? IdPadre { get; set; }

        public AplicacionEntity Aplicacion { get; set; }
        
        public ElementoAplicacionEntity? Padre { get; set; }

        public TipoElementoAplicacionEntity TipoElementoAplicacion { get; set; }

        public ICollection<ElementoAplicacionEntity> Descendientes { get; set; }

        public ICollection<RelacionGrupoUsuarioElementoAplicacionTipoPermisoAplicacionEntity> GruposPermisos { get; set; }
    }
}
