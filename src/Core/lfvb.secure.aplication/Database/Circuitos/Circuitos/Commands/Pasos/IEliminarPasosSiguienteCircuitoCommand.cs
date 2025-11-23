using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public interface IEliminarPasosSiguienteCircuitoCommand
    {
        public Task<List<Guid>> execute(Guid idPaso, List<Guid> idsPasosSiguientes);
    }
}
