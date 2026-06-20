using lfvb.secure.aplication.Database.Calendario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario
{
    public interface IGetCalendariosUsuarioQuery
    {
        public Task<List<CalendarioModel>> execute(Guid idUsuario);
    }
}
