using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Models
{
    public class CredencialUsuarioModel
    {
        public long Id { get; set; }
        public Guid? IdUsuario { get; set; }
        public TipoCredencialModel Tipo { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}
