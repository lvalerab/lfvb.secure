using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetSituaciones
{
    public class GetSituacionesPersonalesQuery:IGetSituacionesPersonalesQuery
    {
        private readonly IDataBaseService _db;

        public GetSituacionesPersonalesQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<SituacionPersonaModel>> execute(Guid idPersona)
        {
            List<SituacionPersonaModel> situaciones = await (from sp in _db.SituacionesPersona
                                                        where sp.IdPersona == idPersona
                                                        select new SituacionPersonaModel
                                                        {
                                                            Id = sp.Id,
                                                            Tipo = new TipoSituacionPersonaModel
                                                            {
                                                                Codigo = sp.TipoSituacionPersona.Codigo,
                                                                Nombre = sp.TipoSituacionPersona.Nombre
                                                            },
                                                            FechaInicio = sp.FechaDesde,
                                                            FechaFin = sp.FechaHasta??DateTime.Now
                                                        }).ToListAsync(); 
            return situaciones;
        }
    }
}
