using lfvb.secure.domain.Entities.Circuitos.Accion;
using lfvb.secure.domain.Entities.Circuitos.AccionUsuario;
using lfvb.secure.domain.Entities.TipoElemento;

namespace lfvb.secure.domain.Entities.Circuitos.AccionTipoElemento
{
    public class AccionTipoElementoEntity
    {
        public Guid Id { get; set; }                        
        public string CodigoTipoElemento { get; set; }  
        
        public bool LlamarSW { get; set; }  
        public string PuntoAcceso { get; set; } 
        public bool LlamarLibreriaNET { get; set; } 
        public string LibreriaNET { get; set; }
        public string MetodoLibreriaNET { get; set; }
        public bool EsAccionUsuario { get; set; }   
        public string CodigoAccionUsuario { get; set; }


        public AccionEntity Accion { get; set; }
        public TipoElementoEntity TipoElemento { get; set; }
        public AccionUsuarioEntity AccionUsuario { get; set; }
    }
}
