using lfvb.secure.aplication.Database.Censo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Exceptions
{
    public class PersonaEncontradaEnAltaException:Exception
    {
        public IList<PersonaModel> PersonasEncontradas { get; set; }
        public PersonaEncontradaEnAltaException(string message): base(message)
        {
        }

        public PersonaEncontradaEnAltaException(string message, IList<PersonaModel> personasEncontradas): base(message)
        {
            PersonasEncontradas = personasEncontradas;
        }
    }
}
