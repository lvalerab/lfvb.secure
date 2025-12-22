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
    public class GetPasosCircuitoQuery : IGetPasosCircuitoQuery
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetPasosCircuitoQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<PasoModel>> execute(Guid circuitoId)
        {
            List<PasoModel> pasos = await (_db.Pasos.Include(p => p.Circuito)
                                                    .Include(p=>p.Estado)
                                                    .Include(p=>p.EstadoSiguiente)
                                                    .Include(p=>p.CircuitoSiguiente)
                                                    .Include(p=>p.Bandeja)
                                        .Where(p => p.Circuito != null && p.Circuito.Id == circuitoId)
                                        .Select(p => new PasoModel
                                        {
                                            Id = p.Id,
                                            Nombre=p.Nombre,
                                            Circuito = p.Circuito != null ? new CircuitoModel
                                            {
                                                Id = p.Circuito.Id,
                                                Nombre = p.Circuito.Nombre,
                                                Descripcion = p.Circuito.Descripcion
                                            } : null,
                                            Estado = p.Estado != null ? new EstadoModel
                                            {
                                                Codigo = p.Estado.Codigo,
                                                Nombre = p.Estado.Nombre,
                                                Descripcion = p.Estado.Descripcion
                                            } : null,
                                            EstadoNuevo = p.EstadoSiguiente != null ? new EstadoModel
                                            {
                                                Codigo= p.EstadoSiguiente.Codigo,   
                                                Nombre = p.EstadoSiguiente.Nombre,
                                                Descripcion = p.EstadoSiguiente.Descripcion
                                            } : null,
                                            CircuitoSiguiente = p.CircuitoSiguiente != null ? new CircuitoModel
                                            {
                                                Id = p.CircuitoSiguiente.Id,
                                                Nombre = p.CircuitoSiguiente.Nombre,
                                                Descripcion = p.CircuitoSiguiente.Descripcion
                                            } : null,
                                            PasosSiguientes =  (from ps in _db.PasosSiguientes
                                                                                 where p.Id == ps.IdPaso
                                                                                 select ps.IdPasoSiguiente).ToList(),
                                            Bandeja=p.Bandeja!=null?new BandejaTramiteModel { Id=p.Bandeja.Id, Nombre=p.Bandeja.Nombre, Descripcion=p.Bandeja.Descripcion }:null,
                                            UsuariosTramitadores=(from rs in _db.PermisosPasosUsuarios
                                                                  join us in _db.Usuarios on rs.IdUsuario equals us.Id
                                                                  where rs.IdPaso==p.Id
                                                                  select new UsuarioEntity
                                                                  {
                                                                      Id=us.Id,
                                                                      Apellido1=us.Apellido1,
                                                                      Apellido2=us.Apellido2,
                                                                      Nombre=us.Nombre,
                                                                      Usuario=us.Usuario
                                                                  }
                                                                  ).ToList(),
                                            GruposTramitadores=(from rg in _db.PermisosPasosGrupos
                                                                join gr in _db.Grupos on rg.IdGrupoUsuario equals gr.Id
                                                                where rg.IdPaso==p.Id
                                                                select new GrupoModel
                                                                {
                                                                    Id=gr.Id,
                                                                    Nombre=gr.Nombre
                                                                }).ToList()
                                        })).ToListAsync();

            foreach (var paso in pasos)
            {
                var pasosSiguientesIds = await (_db.PasosSiguientes
                                                .Where(ps => ps.IdPaso == paso.Id)
                                                .Select(ps => ps.IdPasoSiguiente))
                                                .ToListAsync();
                paso.PasosSiguientes = pasosSiguientesIds;
            };

            return pasos;
        }
    }
}
