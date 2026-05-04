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
    public class GetTipoRelacionPersonaQuery:IGetTipoRelacionPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetTipoRelacionPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }   

        public async Task<List<TipoRelacionPersonaModel>> execute()
        {
            var result = await (from tr in _db.TiposRelacionesPersona
                                select new TipoRelacionPersonaModel { 
                                    Codigo = tr.Codigo,
                                    Nombre = tr.Nombre  
                                }).ToListAsync();

            return result.ToList();
        }   
    }
}
