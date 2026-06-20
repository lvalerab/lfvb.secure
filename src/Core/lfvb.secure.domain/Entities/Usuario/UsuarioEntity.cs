using lfvb.secure.domain.Entities.Calendario;
using lfvb.secure.domain.Entities.Circuitos.EstadoElemento;
using lfvb.secure.domain.Entities.Circuitos.EstadoElementoSiguiente;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.Hydra;
using lfvb.secure.domain.Entities.RelacionUsuarioGrupoUsuarioAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Usuario
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }

        public String Nombre { get; set; }

        public String Apellido1 { get; set; }

        public String Apellido2 { get; set; }

        public String Usuario { get; set; }

        public String Email { get; set; }   

        public ICollection<CredencialEntity>? Credenciales { get; set; }

        public ICollection<RelacionUsuarioGrupoUsuarioAplicacionEntity>? RelacionGrupos { get; set; }

        public ICollection<PermisoPasoUsuarioEntity>? PermisosPasos { get; set; }   
        
        public ICollection<EstadoElementoEntity>? Tramitadores { get; set; } 

        public ICollection<EstadoElementoSiguienteEntity>? EnvioEstados { get; set; }

        public ICollection<HydraEntity> HydraPropietario { get; set; }

        public ICollection<HydraEntity> HydraEjecutor { get; set; }

        public ICollection<CalendarioUsuarioEntity> Calendarios { get; set; }

        public ICollection<CalendarioUsuarioEntity> CalendariosEntradas { get; set; }

        public ICollection<EntradaCalendarioEntity> EntradasCalendario { get; set; }    

    }
}
