using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Usuario;


namespace lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente
{
    public class EstadoElementoSiguienteEntity
    {
        public long Id { get; set; }    
        public long IdSiguiente { get; set; }   
        public Guid IdUsuarioEnvio { get; set; }
        public DateTime Fecha { get; set; } 

        public EstadoElementoEntity RelacionEstadoActual { get; set; }    
        public EstadoElementoEntity RelacionEstadoSiguiente { get; set; }   
        public UsuarioEntity UsuarioEnvio { get; set; }

    }
}
