using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries
{
    public interface IGetCircuitoQuery
    {
        public Task<CircuitoModel?> execute(Guid idCircuito);
    }
}
