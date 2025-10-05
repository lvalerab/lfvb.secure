using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.GrupoAdministradorCircuito;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.Circuitos.PasoAccion;
using lfvb.secure.domain.Entities.Circuitos.TipoElementoCircuito;
using lfvb.secure.domain.Entities.Circuitos.Tramite;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.TipoElemento;

namespace lfvb.secure.domain.Entities.Circuitos.Circuito
{
    public class CircuitoEntity
    {
        public Guid Id { get; set; }                
        public Guid IdTramite { get; set; }  
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public string Normativa { get; set; }   
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime? FechaBaja { get; set; }


        public TramiteEntity Tramite { get; set; }

        public ICollection<TipoElementoCircuitoEntity> RelacionTiposElementos { get; set; } 
        public ICollection<GrupoAdministradorCircuitoEntity> GruposAdministradores { get; set; }

        public ICollection<PasoEntity> Pasos { get; set; }
        public ICollection<PasoEntity> PasosSiguientes { get; set; }

        public ICollection<EstadoElementoEntity> ElementosEstados { get; set; } 

        public ICollection<PasoAccionEntity> PasosErrores { get; set; }
    }
}
