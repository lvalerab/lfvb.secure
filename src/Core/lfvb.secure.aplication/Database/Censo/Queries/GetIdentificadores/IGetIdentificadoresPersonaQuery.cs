using lfvb.secure.aplication.Database.Censo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetIdentificadores
{
    public interface IGetIdentificadoresPersonaQuery
    {
        public Task<List<IdentificacionPersonaModel>> execute(Guid idPersona);
    }
}
