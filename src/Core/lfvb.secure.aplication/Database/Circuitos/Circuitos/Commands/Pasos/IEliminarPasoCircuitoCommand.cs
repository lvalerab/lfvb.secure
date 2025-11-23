using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public interface IEliminarPasoCircuitoCommand
    {
        public Task<bool> execute(Guid idPaso, bool interconectar=false);
    }
}
