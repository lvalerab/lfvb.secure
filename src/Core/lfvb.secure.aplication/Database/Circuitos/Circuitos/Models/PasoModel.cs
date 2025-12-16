using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.domain.Entities.GrupoUsuarioAplicacion;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Models
{
    public class PasoModel
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public CircuitoModel? Circuito { get; set; }=null;
        public BandejaTramiteModel? Bandeja { get; set; }=null; 
        public string? Nombre { get; set; } = null;
        public EstadoModel? Estado { get; set; }=null;
        public EstadoModel? EstadoNuevo { get; set; }=null;
        public CircuitoModel? CircuitoSiguiente { get; set; }=null;
        public List<Guid>? PasosSiguientes { get; set; }=new List<Guid>();
        public List<UsuarioEntity> UsuariosTramitadores { get; set; } = new List<UsuarioEntity>();
        public List<GrupoModel> GruposTramitadores { get; set; }= new List<GrupoModel>();   
    }
}
