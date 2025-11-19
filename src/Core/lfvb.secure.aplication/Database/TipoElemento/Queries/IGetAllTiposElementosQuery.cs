using lfvb.secure.aplication.Database.TipoElemento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoElemento.Queries
{
    public interface IGetAllTiposElementosQuery
    {

        public Task<List<TipoElementoModel>> Execute();
    }
}
