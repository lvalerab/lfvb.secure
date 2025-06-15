using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Models
{
    public class GrupoModel
    {
        public Guid? Id { get; set; }
        public string? Nombre { get; set; } 
        public AplicacionModel? Aplicacion { get; set; }
    }
}
