using lfvb.secure.aplication.Database.Direcciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Queries
{
    public interface IGetAllTiposEntidadesTerritorialesQuery
    {
        public Task<List<TipoEntidadTerritorialModel>> execute();
    }
}
