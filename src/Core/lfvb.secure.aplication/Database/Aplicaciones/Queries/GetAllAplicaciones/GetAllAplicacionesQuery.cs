using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAllAplicaciones
{
    public class GetAllAplicacionesQuery: IGetAllAplicacionesQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        public GetAllAplicacionesQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;   
        }
        public async Task<List<AplicacionModel>> Execute()
        {
            List<AplicacionModel> aplicaciones = await (from ap in _db.Aplicaciones
                                                        select new AplicacionModel
                                                        {
                                                            Id = ap.Id,
                                                            Codigo = ap.Codigo,
                                                            Nombre = ap.Nombre
                                                        }).ToListAsync<AplicacionModel>();
            return aplicaciones;    
        }
    }
}
