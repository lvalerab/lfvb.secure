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
            PasoModel? paso = await (from p in _db.Pasos
                                                    .Include(p=>p.Estado)
                                                    .Include(p=>p.Circuito)
                                                    .Include(p=>p.EstadoSiguiente)
                                                    .Include(p=>p.CircuitoSiguiente)
                                   where p.Id == pasoId
                                   select new PasoModel
                                   {
                                        Id = p.Id,
                                        Nombre=p.Nombre,
                                        Circuito = new CircuitoModel
                                        {
                                            Id = p.Circuito.Id,
                                            Nombre = p.Circuito.Nombre,
                                            Descripcion = p.Circuito.Descripcion,   
                                            Normativa  = p.Circuito.Normativa
                                        },
                                        Estado = new EstadoModel
                                        {
                                            Codigo = p.Estado.Codigo,
                                            Nombre = p.Estado.Nombre,
                                            Descripcion = p.Estado.Descripcion
                                        },
                                        EstadoNuevo = new EstadoModel
                                        {
                                            Codigo = p.EstadoSiguiente.Codigo,
                                            Nombre = p.EstadoSiguiente.Nombre,
                                            Descripcion = p.EstadoSiguiente.Descripcion
                                        },
                                        CircuitoSiguiente = new CircuitoModel
                                        {
                                            Id = p.CircuitoSiguiente.Id,
                                            Nombre = p.CircuitoSiguiente.Nombre,
                                            Descripcion = p.CircuitoSiguiente.Descripcion,
                                            Normativa = p.CircuitoSiguiente.Normativa
                                        },
                                        PasosSiguientes = (from ps in _db.PasosSiguientes
                                                             where p.Id == ps.IdPaso 
                                                           select ps.IdPasoSiguiente).ToList()
                                   }).FirstOrDefaultAsync();

            return paso;
        }
    }
}
