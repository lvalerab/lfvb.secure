using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.domain.Entities.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.AltaCalendario
{
    public interface IAltaCalendarioUsuarioCommand
    {
        public Task<CalendarioModel> execute(CalendarioModel calendarioModel);  
    }
}
