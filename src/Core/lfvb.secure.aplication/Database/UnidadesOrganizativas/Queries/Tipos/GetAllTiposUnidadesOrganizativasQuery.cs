using AutoMapper;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Tipos
{
    public class GetAllTiposUnidadesOrganizativasQuery : IGetAllTiposUnidadesOrganizativasQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetAllTiposUnidadesOrganizativasQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        /// <summary>
        /// Obtiene los tipos de unidades organizativas de la aplicacion
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipoUnidadOrganizativaModel>> execute()
        {
            List<TipoUnidadOrganizativaModel> tipos= await (from to in _db.TiposUnidadesOrganizativas
                                                           select new TipoUnidadOrganizativaModel
                                                           {
                                                               Codigo = to.Codigo,
                                                               Nombre = to.Nombre,
                                                               Descripcion = to.Descripcion
                                                           }).ToListAsync();

            return tipos;
        }
    }
}
