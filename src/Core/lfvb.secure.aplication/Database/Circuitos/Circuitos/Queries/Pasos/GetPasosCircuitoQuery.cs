using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
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
                                                                                 select ps.IdPasoSiguiente).ToList()
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
