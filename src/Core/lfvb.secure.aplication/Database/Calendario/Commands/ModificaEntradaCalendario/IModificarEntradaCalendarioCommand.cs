using lfvb.secure.aplication.Database.Calendario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.ModificaEntradaCalendario
{
    public interface IModificarEntradaCalendarioCommand
    {
        public Task<EntradaCalendarioModel> execute(EntradaCalendarioModel model);
    }
}
