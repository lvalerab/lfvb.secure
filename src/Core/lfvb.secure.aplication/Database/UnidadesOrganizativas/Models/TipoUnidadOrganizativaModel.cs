using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Models
{
    public class TipoUnidadOrganizativaModel
    {
        public Guid? Codigo { get; set; }   
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
