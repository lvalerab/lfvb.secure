using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Elementos.Commands
{
    public interface IAltaElementoCommand
    {
        public Task<Guid> execute(string codigoTipoElemento, bool commit=false);
    }
}
