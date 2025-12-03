using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Estados
{
    public class EstadosElementosQuery:IEstadosElementosQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public EstadosElementosQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<EstadoModel>> execute()
        {
            List<EstadoModel> estados = await (from es in _db.Estados
                                              select new EstadoModel
                                              {
                                                  Codigo = es.Codigo,
                                                  Nombre = es.Nombre,
                                                  Descripcion = es.Descripcion
                                              }).ToListAsync();
            return estados;
        }
    }
}
