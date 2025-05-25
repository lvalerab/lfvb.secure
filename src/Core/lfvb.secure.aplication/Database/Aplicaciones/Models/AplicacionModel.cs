using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class AplicacionModel
    {
        public Guid? Id { get; set; }   
        public string? Codigo { get; set; }
        public string? Nombre { get; set; } 
        public List<ElementoAplicacionModel> Elementos { get; set; } = new List<ElementoAplicacionModel>();
        public List<GrupoModel> Grupos { get; set; } = new List<GrupoModel>();  
    }
}
