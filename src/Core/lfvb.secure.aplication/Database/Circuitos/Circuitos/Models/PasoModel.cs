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
        public string? Nombre { get; set; } = null;
        public EstadoModel? Estado { get; set; }=null;
        public EstadoModel? EstadoNuevo { get; set; }=null;
        public CircuitoModel? CircuitoSiguiente { get; set; }=null;
        public List<Guid>? PasosSiguientes { get; set; }=new List<Guid>();
    }
}
