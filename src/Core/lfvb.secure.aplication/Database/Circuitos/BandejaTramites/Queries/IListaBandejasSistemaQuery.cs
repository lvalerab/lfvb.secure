using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Queries
{
    public interface IListaBandejasSistemaQuery
    {
        Task<List<BandejaTramiteModel>> execute();
    }
}
