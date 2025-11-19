using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands
{
    public interface IAltaCircuitoCommand
    {
        public Task<AltaCircuitoModel> Execute(AltaCircuitoModel model);
    }
}
