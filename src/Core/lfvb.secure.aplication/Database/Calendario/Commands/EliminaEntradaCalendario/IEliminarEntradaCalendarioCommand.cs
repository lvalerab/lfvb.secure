using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.EliminaEntradaCalendario
{
    public interface IEliminarEntradaCalendarioCommand
    {
        public Task<bool> execute(Guid idEntradaCalendario);
    }
}
