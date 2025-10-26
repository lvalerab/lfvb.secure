using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite
{
    public interface IGetTramiteQuery
    {
        public Task<TramiteModel> Execute(Guid id);
    }
}
