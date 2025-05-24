using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAllAplicaciones
{
    public interface IGetAllAplicacionesQuery
    {
        public Task<List<AplicacionModel>> Execute();
    }
}
