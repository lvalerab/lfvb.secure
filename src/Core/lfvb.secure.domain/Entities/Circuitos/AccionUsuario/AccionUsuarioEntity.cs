using lfvb.secure.domain.Entities.Circuitos.AccionTipoElemento;

namespace lfvb.secure.domain.Entities.Circuitos.AccionUsuario
{
    public class AccionUsuarioEntity
    {
        public string Codigo { get; set; }  
        public string Nombre { get; set; }  

        public ICollection<AccionTipoElementoEntity> AccionesTipoElemento { get; set; }
    }
}
