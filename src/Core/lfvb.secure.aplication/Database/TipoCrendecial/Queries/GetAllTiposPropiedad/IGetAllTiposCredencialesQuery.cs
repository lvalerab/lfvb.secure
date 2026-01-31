using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoCrendecial.Queries.GetAllTiposCredenciales
{
    public interface IGetAllTiposCredencialesQuery
    {
        public Task<List<TipoCredencialModel>> Execute();  
    }
}
