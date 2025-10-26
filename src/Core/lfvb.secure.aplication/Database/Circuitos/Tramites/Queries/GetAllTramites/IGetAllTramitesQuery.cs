using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites
{
    public interface IGetAllTramitesQuery
    {
        public Task<List<TramiteModel>> Execute();
        public Task<List<TramiteModel>> Execute(int page, int registros);
    }
}
