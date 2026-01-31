using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Models
{
    public class UnidadOrganizativaModel
    {
        public Guid? Codigo { get; set; }
        public string? Nombre { get; set; }
        public TipoUnidadOrganizativaModel? TipoUnidadOrganizativa { get; set; }
        public UnidadOrganizativaModel? Padre { get; set; }   
        public List<UnidadOrganizativaModel>? Unidades { get; set; }
    }
}
