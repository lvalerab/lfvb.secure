using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Estados
{
    public interface IEstadosElementosQuery
    {
        public Task<List<EstadoModel>> execute();
    }
}
