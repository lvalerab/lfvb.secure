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
    public class GetAllTiposPersonaQuery: IGetAllTiposPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetAllTiposPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<TipoPersonaModel>> execute()
        {
            var result = await (from tp in _db.TiposPersonas
                                select new TipoPersonaModel
                                {
                                    Codigo = tp.Codigo,
                                    Nombre = tp.Nombre
                                }).ToListAsync();

            return result;
        }
    }
}
