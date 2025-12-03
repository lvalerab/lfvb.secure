using lfvb.secure.aplication.Database.Circuitos.Acciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Acciones.Queries
{
    public interface IGetAllAccionesQuery
    {
        public Task<List<AccionModel>> execute();
    }
}
