using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Models
{
    public class UsuarioModel
    {
        public Guid? Id { get; set; }
        public string? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido1 { get; set; }  
        public string? Apellido2 { get; set; }
        public List<CredencialUsuarioModel> Credenciales { get; set; } = new List<CredencialUsuarioModel>();
        public List<GrupoModel> Grupos { get; set; } = new List<GrupoModel>();
    }
}
