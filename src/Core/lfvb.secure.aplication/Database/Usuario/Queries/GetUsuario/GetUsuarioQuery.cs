using AutoMapper;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQuery : IGetUsuarioQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetUsuarioQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<UsuarioModel> Execute(Guid? id)
        {
            UsuarioModel usuario=await (from u in _db.Usuarios
                                                              .Include(u=>u.Credenciales)
                                                              .Include(u=>u.RelacionGrupos)
                                        where u.Id.Equals(id)
                                        select new UsuarioModel
                                        {
                                            Id = u.Id,
                                            Usuario=u.Usuario,
                                            Nombre = u.Nombre,
                                            Apellido1 = u.Apellido1,
                                            Apellido2 = u.Apellido2,
                                            Email = u.Email,
                                            Credenciales = u.Credenciales.Select(c => new CredencialUsuarioModel
                                            {
                                                Id = c.Id,
                                                Tipo = new TipoCredencialModel
                                                {
                                                    Codigo=c.TipoCredencial.Codigo,
                                                    Nombre = c.TipoCredencial.Nombre
                                                },
                                                Desde = c.VigenteDesde,
                                                Hasta = c.VigenteHasta
                                            }).ToList(),
                                            Grupos = u.RelacionGrupos.Select(g => new GrupoModel
                                            {
                                                Id = g.Grupo.Id,
                                                Nombre = g.Grupo.Nombre
                                            }).ToList()
                                        }).FirstOrDefaultAsync<UsuarioModel>();

            return usuario;
        }
    }
}
