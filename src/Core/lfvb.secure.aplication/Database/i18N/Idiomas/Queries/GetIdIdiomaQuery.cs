using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Queries
{
    public class GetIdIdiomaQuery: IGetIdIdiomaQuery
    {
        private readonly IDataBaseService _db;

        public GetIdIdiomaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<Guid?> execute(string codigo)
        {
            Guid? ident = await (from id in _db.Idiomas
                              where id.Codigo == codigo
                              select id.Id).FirstOrDefaultAsync();

            return ident;
        }
    }
}
