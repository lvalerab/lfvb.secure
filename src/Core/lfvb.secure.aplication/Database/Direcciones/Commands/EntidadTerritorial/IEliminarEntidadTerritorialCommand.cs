using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial
{
    public interface IEliminarEntidadTerritorialCommand
    {
        public Task<bool> execute(Guid id);   
    }
}
