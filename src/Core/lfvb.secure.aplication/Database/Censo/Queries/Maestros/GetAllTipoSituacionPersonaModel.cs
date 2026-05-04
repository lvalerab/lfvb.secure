using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.Maestros
{
    public class GetAllTipoSituacionPersonaModel : IGetAllTipoSituacionPersonaModel
    {
        private readonly IDataBaseService _db;
        public GetAllTipoSituacionPersonaModel(IDataBaseService db)
        {
            _db = db;
        }
        public async Task<List<TipoSituacionPersonaModel>> execute()
        {
            var result = await (from tsp in _db.TiposSituacionesPersona
                                select new TipoSituacionPersonaModel
                                {
                                    Codigo = tsp.Codigo,
                                    Nombre = tsp.Nombre
                                }).ToListAsync();
            return result;

        }
    }
}
