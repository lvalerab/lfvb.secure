using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Models
{
    public class FiltroBusquedaPersonasModel
    {
        public Guid? Id { get; set; }=null; 
        public string? Nombre { get; set; }=null;
        public string? Apellido1 { get; set; }=null;
        public string? Apellido2 { get; set; }=null;
        public List<IdentificacionPersonaModel> Identificaciones { get; set; }=new List<IdentificacionPersonaModel>();  
    }
}
