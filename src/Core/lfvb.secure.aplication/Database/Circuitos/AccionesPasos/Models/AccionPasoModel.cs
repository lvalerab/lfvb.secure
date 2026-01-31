using lfvb.secure.aplication.Database.Circuitos.Acciones.Models;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Models
{
    public class AccionPasoModel
    {
        public long? Id { get; set; } 
        public PasoModel Paso { get; set; }=new PasoModel();
        public string? TipoEjecucion { get; set; } = "PREPASO";
        public int? Orden { get; set; } = 0;
        public AccionModel Accion { get; set; }=new AccionModel();
        public CircuitoModel? CircuitoError { get; set; }=null; 
    }
}
