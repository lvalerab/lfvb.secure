using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.EliminarCalendario
{
    public interface IEliminarCalendarioCommand
    {
        public Task<bool> execute(Guid idCalendario, Guid? idCalendarioDestino = null);
    }
}
