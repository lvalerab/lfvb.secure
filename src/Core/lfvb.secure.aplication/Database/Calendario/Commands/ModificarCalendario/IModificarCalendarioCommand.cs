using lfvb.secure.aplication.Database.Calendario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.ModificarCalendario
{
    public interface IModificarCalendarioCommand
    {
        public Task<CalendarioModel> execute(CalendarioModel calendarioModel);
    }
}
