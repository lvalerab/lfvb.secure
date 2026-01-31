using lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Queries
{
    public interface IGetAccionesPasoQuery
    {
        public Task<List<AccionPasoModel>> execute(Guid idPaso, string tipo="");    
    }
}
