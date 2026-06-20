using lfvb.secure.aplication.Database.Calendario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Queries.GetEntradasCalendario
{
    public interface IGetEntradaCalendario
    {
        public Task<List<EntradaCalendarioModel>> execute(Guid idCalendario, DateTime? fechaInicio = null, DateTime? FechaFin = null);
    }
}
