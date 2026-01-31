using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Models
{
    public class CredencialModel
    {
        public Int32 Id { get; set; }   
        public UsuarioModel Usuario { get; set; }
        public TipoCredencialModel Tipo { get; set; }   
        public DateTime VigenteDesde { get; set; }
        public DateTime? VigenteHasta { get; set; }
        public PasswordCredencial Password { get; set; }
        public TokenCredencial Token { get; set; }
    }
}
