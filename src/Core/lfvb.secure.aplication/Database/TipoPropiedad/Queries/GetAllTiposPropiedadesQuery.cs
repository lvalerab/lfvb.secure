using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.TipoPropiedad.Queries
{
    public class GetAllTiposPropiedadesQuery:IGetAllTiposPropiedadesQuery
    {
        private readonly IMapper _mapper;
        private readonly IDataBaseService _db;
        public GetAllTiposPropiedadesQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }


        public async Task<List<TipoPropiedadModel>> Execute()
        {
            List<TipoPropiedadModel> tipos = await (from tp in _db.TiposPropiedades
                                                    select new TipoPropiedadModel
                                                    {
                                                        Codigo = tp.Codigo,
                                                        Nombre = tp.Nombre,
                                                        Multiple = tp.Multiple.Equals("S"),
                                                        Historico = tp.Historico.Equals("S"),
                                                        Intervalo = tp.Intervalo.Equals("S"),
                                                        Tipo = tp.Tipo,
                                                        ListaValores = tp.ListaValores.Equals("S")
                                                    }).ToListAsync<TipoPropiedadModel>();
            return tipos;
        }
    }
}
