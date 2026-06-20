using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Models
{
    public class CalendarioModel
    {
        public Guid? Id { get; set; }   
        public UsuarioModel Usuario { get; set; }
        public string Nombre { get; set; }  
        public List<EntradaCalendarioModel> Entradas { get; set; } = new List<EntradaCalendarioModel>();    
    }
}
