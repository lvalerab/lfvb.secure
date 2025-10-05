using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.Paso;

namespace lfvb.secure.domain.Entities.Circuitos.Estado
{
    public class EstadoEntity
    {
        public string Codigo { get; set; }  
        public string Nombre { get; set; }  
        public string Descripcion { get; set; } 

        public ICollection<PasoEntity> Pasos { get; set; }
        public ICollection<PasoEntity> PasosSiguientes { get; set; }
        public ICollection<EstadoElementoEntity> EstadosElemento { get; set; }
    }
}
