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
    public class GetAllTiposIdentificadoresPersonaQuery: IGetAllTiposIdentificadoresPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetAllTiposIdentificadoresPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<TipoIdentificacionPersonaModel>> execute()
        {
            var result = await (from ti in _db.TiposIdentificadoresPersona
                                select new TipoIdentificacionPersonaModel
                                {
                                    Codigo = ti.Codigo,
                                    Nombre = ti.Nombre
                                }).ToListAsync();

            return result;
        }
    }
}   