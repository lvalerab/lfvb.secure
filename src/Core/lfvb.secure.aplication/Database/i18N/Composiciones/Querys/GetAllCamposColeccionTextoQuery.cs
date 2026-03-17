using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Querys
{
    public class GetAllCamposColeccionTextoQuery: IGetAllCamposColeccionTextoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetAllCamposColeccionTextoQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<CampoColeccionTextoModel>> execute(Guid idColeccion)
        {
            List<CampoColeccionTextoModel> result = await (from cm in _db.CamposTextos.Include(cm=>cm.Coleccion)
                                                           where cm.IdColeccion == idColeccion
                                                            select new CampoColeccionTextoModel
                                                            {
                                                                Id = cm.Id,
                                                                Nombre = cm.Nombre,
                                                                Coleccion = new ColeccionTextoModel
                                                                {
                                                                    Id = cm.Coleccion.Id,
                                                                    Nombre = cm.Coleccion.Nombre
                                                                }
                                                            }).ToListAsync();

            return result;  
        }
    }
}
