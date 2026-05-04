using lfvb.secure.aplication.Database.Censo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.ModificaPersona
{
    public interface IModificarPersonaCommand
    {
        public Task<PersonaModel> execute(PersonaModel persona);    
    }
}
