using lfvb.secure.domain.Entities.Circuitos.Circuito;

namespace lfvb.secure.domain.Entities.Circuitos.Tramite
{
    public class TramiteEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Normativa { get; set; }  


        public ICollection<CircuitoEntity> Circuitos { get; set; }  
    }
}
