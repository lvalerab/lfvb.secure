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
    public class GetAllTipoSexoPersonaQuery : IGetAllTipoSexoPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetAllTipoSexoPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<TipoSexoPersonaModel>> execute()
        {
            return await _db.TiposSexoPersona
                            .Select(ts => new TipoSexoPersonaModel
                            {
                                Codigo = ts.Codigo,
                                Nombre = ts.Nombre
                            }).ToListAsync();
        }
    }
}       