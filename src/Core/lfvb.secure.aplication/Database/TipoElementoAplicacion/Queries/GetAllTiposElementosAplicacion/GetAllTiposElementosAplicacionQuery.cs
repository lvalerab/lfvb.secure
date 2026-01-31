using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoElementoAplicacion.Queries.GetAllTiposElementosAplicacion
{
    public class GetAllTiposElementosAplicacionQuery : IGetAllTiposElementosAplicacionQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetAllTiposElementosAplicacionQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<TipoElementoAplicacionModel>> Execute()
        {
            List<TipoElementoAplicacionModel> tiposElementosAplicacion = await (from tea in _db.TiposElementosAplicaciones
                                                                              select new TipoElementoAplicacionModel
                                                                              {
                                                                                  Codigo = tea.Codigo,
                                                                                  Nombre = tea.Nombre
                                                                              }).ToListAsync();
            return tiposElementosAplicacion;
        }
    }
}
