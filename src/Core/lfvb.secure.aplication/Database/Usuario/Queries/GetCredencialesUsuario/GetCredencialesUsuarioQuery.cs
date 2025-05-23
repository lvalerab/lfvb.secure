using AutoMapper;
using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.GetCredencialesUsuario
{
    public class GetCredencialesUsuarioQuery:IGetCredencialesUsuarioQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        public GetCredencialesUsuarioQuery(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }
        public async Task<List<CredencialUsuarioModel>> Execute(Guid idUsuario)
        {
            List<CredencialUsuarioModel> credenciales = await (from c in _db.Credenciales
                                                                where c.IdUsuario.Equals(idUsuario)
                                                                select new CredencialUsuarioModel
                                                                {
                                                                    Id = c.Id,
                                                                    IdUsuario = c.IdUsuario,
                                                                    Tipo = new TipoCredencialModel
                                                                    {
                                                                        Codigo = c.TipoCredencial.Codigo,
                                                                        Nombre = c.TipoCredencial.Nombre
                                                                    },
                                                                    Desde = c.VigenteDesde,
                                                                    Hasta = c.VigenteHasta
                                                                }).ToListAsync<CredencialUsuarioModel>();
            return credenciales;
        }
    }   
}
