using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Queries
{
    public class GetAllTiposViasQuery: IGetAllTiposViasQuery
    {
        private readonly IDataBaseService _db;

        public GetAllTiposViasQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<TipoViaModel>> execute()
        {
            List<TipoViaModel> resultado=await (from tv in _db.TiposVias
                                                select new TipoViaModel
                                                {
                                                    Codigo = tv.Codigo,
                                                    Nombre = tv.Nombre
                                                }).ToListAsync();

            return resultado;        
        }
    }
}
