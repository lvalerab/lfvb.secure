using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Querys
{
    public class GetAllColeccionesTexto: IGetAllColeccionesTexto
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetAllColeccionesTexto(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }   

        public async Task<List<ColeccionTextoModel>> execute()
        {
            List<ColeccionTextoModel> resultado=await (from ct in _db.ColeccionesTextos
                                                      select new ColeccionTextoModel
                                                      {
                                                          Id = ct.Id,
                                                          Nombre = ct.Nombre,
                                                          Detalle = ct.Descripcion
                                                      }).ToListAsync(); 

            return resultado;
        }
    }
}
