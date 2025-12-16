using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Models;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries.Pasos
{
    public class GetPasoCircuitoQuery : IGetPasoCircuitoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetPasoCircuitoQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PasoModel?> execute(Guid pasoId)
        {
            //TODO: Ver uso de lateral en mariadb para optimizar esta consulta
            //PasoModel? paso = await (from p in _db.Pasos
            //                                        //.Include(p=>p.Bandeja)
            //                                        .Include(p => p.Estado)
            //                                        .Include(p => p.Circuito)
            //                                        .Include(p => p.EstadoSiguiente)
            //                                        .Include(p => p.CircuitoSiguiente)
            //                                        .Include(p => p.PermisoUsuarios)
            //                                        .Include(p => p.PermisosGrupos)
            //                         where p.Id == pasoId
            //                         select new PasoModel
            //                         {
            //                             Id = p.Id,
            //                             Nombre = p.Nombre,
            //                             Circuito = new CircuitoModel
            //                             {
            //                                 Id = p.Circuito.Id,
            //                                 Nombre = p.Circuito.Nombre,
            //                                 Descripcion = p.Circuito.Descripcion,
            //                                 Normativa = p.Circuito.Normativa
            //                             },
            //                             Bandeja = p.Bandeja != null ? new BandejaTramiteModel
            //                             {
            //                                 Id = p.Bandeja.Id,
            //                                 Nombre = p.Bandeja.Nombre,
            //                                 Descripcion = p.Bandeja.Descripcion
            //                             } : null,
            //                             Estado = new EstadoModel
            //                             {
            //                                 Codigo = p.Estado.Codigo,
            //                                 Nombre = p.Estado.Nombre,
            //                                 Descripcion = p.Estado.Descripcion
            //                             },
            //                             EstadoNuevo = new EstadoModel
            //                             {
            //                                 Codigo = p.EstadoSiguiente.Codigo,
            //                                 Nombre = p.EstadoSiguiente.Nombre,
            //                                 Descripcion = p.EstadoSiguiente.Descripcion
            //                             },
            //                             CircuitoSiguiente = new CircuitoModel
            //                             {
            //                                 Id = p.CircuitoSiguiente.Id,
            //                                 Nombre = p.CircuitoSiguiente.Nombre,
            //                                 Descripcion = p.CircuitoSiguiente.Descripcion,
            //                                 Normativa = p.CircuitoSiguiente.Normativa
            //                             },
            //                             PasosSiguientes = (from ps in _db.PasosSiguientes
            //                                                where p.Id == ps.IdPaso
            //                                                select ps.IdPasoSiguiente).ToList(),
            //                             UsuariosTramitadores = (from us in _db.Usuarios
            //                                                     join pu in p.PermisoUsuarios on us.Id equals pu.IdUsuario
            //                                                     select new UsuarioEntity
            //                                                     {
            //                                                         Id = us.Id,
            //                                                         Nombre = us.Nombre,
            //                                                         Apellido1 = us.Apellido1,
            //                                                         Apellido2 = us.Apellido2
            //                                                     }).ToList(),
            //                             GruposTramitadores = (from gr in _db.Grupos
            //                                                   join pg in p.PermisosGrupos on gr.Id equals pg.IdGrupoUsuario
            //                                                   select new GrupoModel
            //                                                   {
            //                                                       Id = gr.Id,
            //                                                       Nombre = gr.Nombre
            //                                                   }).ToList()
            //                         }).FirstOrDefaultAsync();

            PasoModel? paso = await (from p in _db.Pasos
                                     where p.Id == pasoId
                                     select new PasoModel
                                     {
                                         Id = p.Id,
                                         Nombre = p.Nombre,
                                         Circuito = new CircuitoModel { Id = p.IdCircuito },
                                         Bandeja = p.Bandeja != null ? new BandejaTramiteModel { Id = p.IdBandeja } : null,
                                         Estado = new EstadoModel { Codigo = p.CodEstado },
                                         EstadoNuevo = p.EstadoSiguiente != null ? new EstadoModel { Codigo = p.CodEstadoSiguiente } : null,
                                         CircuitoSiguiente = p.CircuitoSiguiente != null ? new CircuitoModel { Id = p.IdCircuitoSiguiente } : null,
                                         PasosSiguientes = (from ps in _db.PasosSiguientes
                                                            where p.Id == ps.IdPaso
                                                            select ps.IdPasoSiguiente).ToList()
                                     }
                       ).FirstOrDefaultAsync();
            if (paso != null)
            {
                paso.Circuito = await _db.Circuitos
                                                    .Where(c => c.Id == paso.Circuito.Id)
                                                    .Select(c => new CircuitoModel
                                                    {
                                                        Id = c.Id,
                                                        Nombre = c.Nombre,
                                                        Descripcion = c.Descripcion,
                                                        Normativa = c.Normativa
                                                    }).FirstOrDefaultAsync();
                if (paso.Bandeja != null)
                {
                    paso.Bandeja = await _db.BandejasTramites
                                                        .Where(b => b.Id == paso.Bandeja.Id)
                                                        .Select(b => new BandejaTramiteModel
                                                        {
                                                            Id = b.Id,
                                                            Nombre = b.Nombre,
                                                            Descripcion = b.Descripcion
                                                        }).FirstOrDefaultAsync();
                }

                if (paso.CircuitoSiguiente != null)
                {
                    paso.CircuitoSiguiente = await _db.Circuitos
                                                        .Where(c => c.Id == paso.CircuitoSiguiente.Id)
                                                        .Select(c => new CircuitoModel
                                                        {
                                                            Id = c.Id,
                                                            Nombre = c.Nombre,
                                                            Descripcion = c.Descripcion,
                                                            Normativa = c.Normativa
                                                        }).FirstOrDefaultAsync();
                }

                paso.UsuariosTramitadores = await (from us in _db.Usuarios
                                                   join pu in _db.PermisosPasosUsuarios on us.Id equals pu.IdUsuario
                                                   where pu.IdPaso == paso.Id
                                                   select new UsuarioEntity
                                                   {
                                                       Id = us.Id,
                                                       Nombre = us.Nombre,
                                                       Apellido1 = us.Apellido1,
                                                       Apellido2 = us.Apellido2
                                                   }).ToListAsync();

                paso.GruposTramitadores = await (from gr in _db.Grupos
                                                 join pg in _db.PermisosPasosGrupos on gr.Id equals pg.IdGrupoUsuario
                                                 where pg.IdPaso == paso.Id
                                                 select new GrupoModel
                                                 {
                                                     Id = gr.Id,
                                                     Nombre = gr.Nombre
                                                 }).ToListAsync();
            }

            return paso;
        }
    }
}
