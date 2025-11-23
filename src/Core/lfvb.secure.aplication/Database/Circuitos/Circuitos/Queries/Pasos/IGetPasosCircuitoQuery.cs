using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries.Pasos
{
    public interface IGetPasosCircuitoQuery
    {
        public Task<List<PasoModel>> execute(Guid circuitoId);
    }
}
