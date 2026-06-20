using lfvb.secure.aplication.Database.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Models
{
    public class EntradaCalendarioModel
    {
        public Guid? Id { get; set; }
        public TipoEntradaCalendarioModel TipoEntrada { get; set; }
        public UsuarioModel Creador { get; set; }   
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<ParticipanteEntradaCalendarioModel> Participantes { get; set; } = new List<ParticipanteEntradaCalendarioModel>();
    }
}
