using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.ElementosUsuario
{
    public class GetAllElementosUsuario : IGetAllElementosUsuario
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetAllElementosUsuario(IDataBaseService db, IMapper mp) {
            this._db = db;
            this._mapper = mp;
        }

        public async Task<List<VMElementosModel>> Execute(Guid idUsuario)
        {
            List<VMElementosModel> elementos=await (from vw in _db.VistaElementos
                                                    where vw.IdUsuario.Equals(idUsuario)
                                                    select  new VMElementosModel
                                                    {
                                                        Id=vw.Id,
                                                        Etiqueta=vw.Etiqueta,
                                                        Tipo=vw.Tipo
                                                    }).ToListAsync<VMElementosModel>();
            return elementos;
        }
    }
}
