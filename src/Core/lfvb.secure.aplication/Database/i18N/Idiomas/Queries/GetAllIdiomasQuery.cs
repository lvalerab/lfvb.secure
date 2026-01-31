using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Queries
{
    public class GetAllIdiomasQuery : IGetAllIdiomasQuery
    {
        private IDataBaseService _db;
        private IMapper _mp;

        public GetAllIdiomasQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<IdiomaModel>> execute(bool todos = true)
        {
            List<IdiomaModel> rst= await (from i in _db.Idiomas.Include(i=>i.Agrupaciones)
                                    where (todos || (i.Agrupaciones.Count()<=0))
                                        select new IdiomaModel
                                        {
                                            Codigo = i.Codigo,
                                            Nombre = i.Nombre,
                                            Multiple =(i.Agrupaciones.Count()>0)
                                        }).ToListAsync();

            return rst;
        }
    }
}
