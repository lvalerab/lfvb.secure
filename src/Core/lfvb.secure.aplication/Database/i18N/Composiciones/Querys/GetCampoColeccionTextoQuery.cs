using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Querys
{
    public class GetCampoColeccionTextoQuery: IGetCampoColeccionTextoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetCampoColeccionTextoQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<CampoColeccionTextoModel?> execute(Guid idCampoColeccionTexto)
        {
            CampoColeccionTextoModel? result = await (from ct in _db.CamposTextos.Include(x => x.Coleccion)  
                                                     where ct.Id == idCampoColeccionTexto
                                                   select new CampoColeccionTextoModel
                                                   {
                                                       Id = ct.Id,
                                                       Nombre = ct.Nombre,
                                                       Coleccion = new ColeccionTextoModel
                                                       {
                                                           Id = ct.Coleccion.Id,
                                                           Nombre = ct.Coleccion.Nombre
                                                       }
                                                   }).FirstOrDefaultAsync();

            return result;  
        }
    }
}
