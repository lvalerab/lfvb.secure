using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Database.i18N.Textos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Querys
{
    public class GetAllOpcionesCamposColeccionQuery : IGetAllOpcionesCamposColeccionQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        public GetAllOpcionesCamposColeccionQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }


        public async Task<List<OpcionCampoColeccionTextoModel>> execute(Guid idCampoColeccionTexto)
        {
            List<OpcionCampoColeccionTextoModel> result = await (from oc in _db.OpcionesTextos.Include(op => op.Campo)
                                                                 where oc.IdCampo == idCampoColeccionTexto
                                                                 select new OpcionCampoColeccionTextoModel
                                                                 {
                                                                     Id = oc.Id,
                                                                     Campo = new CampoColeccionTextoModel
                                                                     {
                                                                         Id = oc.Campo.Id,
                                                                         Nombre = oc.Campo.Nombre,
                                                                     },
                                                                     Texto = new TextoModel
                                                                     {
                                                                         Id = oc.Texto.Id
                                                                     }
                                                                 }).ToListAsync();
            return result;
        }
    }
}
