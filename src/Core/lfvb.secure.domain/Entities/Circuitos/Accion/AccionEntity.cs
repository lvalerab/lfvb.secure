using lfvb.secure.domain.Entities.Circuitos.AccionTipoElemento;
using lfvb.secure.domain.Entities.Circuitos.PasoAccion;

namespace lfvb.secure.domain.Entities.Circuitos.Accion
{
    public class AccionEntity
    {
        public Guid Id { get; set; }                
        public string Nombre { get; set; }  

        public ICollection<AccionTipoElementoEntity> AccionesTipoElemento { get; set; }
        public ICollection<PasoAccionEntity> Pasos { get; set; }
    }
}
