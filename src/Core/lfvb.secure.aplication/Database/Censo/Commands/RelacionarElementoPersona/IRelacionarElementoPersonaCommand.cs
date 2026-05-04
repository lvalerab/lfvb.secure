using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.RelacionarElementoPersona
{
    public interface IRelacionarElementoPersonaCommand
    {
        public Task<Boolean> execute(Guid idPersona, Guid idElemento, bool commit=true);
    }
}
