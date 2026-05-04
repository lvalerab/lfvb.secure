using lfvb.secure.aplication.Database.Censo.Models;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.AgregarIdentificacion
{
    public interface IAltaModificacionIdentificacionPersonaCommand
    {
        public Task<IdentificacionPersonaModel> execute(IdentificacionPersonaModel identificacion, bool commit=true);
    }
}
