using lfvb.secure.domain.Entities.Circuitos.BandejaTramite;
using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.Circuitos.Estado;
using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.PasoAccion;
using lfvb.secure.domain.Entities.Circuitos.PasoSiguiente;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoGrupo;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario;

namespace lfvb.secure.domain.Entities.Circuitos.Paso
{
    public class PasoEntity
    {
        public Guid Id { get; set; }
        public Guid IdCircuito { get; set; }
        public string CodEstado { get; set; } = string.Empty;
        public string CodEstadoSiguiente { get; set; } = string.Empty;
        public string Nombre { get; set; }
        public Guid? IdCircuitoSiguiente { get; set; }
        public Guid? IdBandeja { get; set; }    

        public CircuitoEntity Circuito { get; set; }
        public EstadoEntity Estado { get; set; }
        public EstadoEntity EstadoSiguiente { get; set; }   
        public CircuitoEntity CircuitoSiguiente { get; set; }
        public BandejaTramiteEntity Bandeja { get; set; }

        public ICollection<PermisoPasoGrupoEntity> PermisosGrupos { get;set; }
        public ICollection<PermisoPasoUsuarioEntity> PermisoUsuarios { get; set; }  
        public ICollection<PasoAccionEntity> Acciones { get; set; }

        public ICollection<PasoSiguienteEntity> PasosPrevios { get; set; }
        public ICollection<PasoSiguienteEntity> PasosSiguientes { get; set; }

        public ICollection<EstadoElementoEntity> EstadosElementos { get; set; }
    }
}
