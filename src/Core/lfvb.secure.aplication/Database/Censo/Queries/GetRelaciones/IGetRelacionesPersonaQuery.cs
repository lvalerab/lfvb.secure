using lfvb.secure.aplication.Database.Censo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetRelaciones
{
    public interface IGetRelacionesPersonaQuery
    {
        public Task<List<RelacionPersonaModel>> execute(Guid idPersona);    
    }
}
