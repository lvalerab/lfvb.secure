using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class GetAplicacionUsuarioModel
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string Nombre { get; set; }
    }
}
