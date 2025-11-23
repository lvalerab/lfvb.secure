using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.PasoSiguiente;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public class AltaPasoSiguienteCircuitoCommand : IAltaPasoSiguienteCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AltaPasoSiguienteCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<Guid>> execute(Guid idPaso, List<Guid> idsPasosSiguientes)
        {
            List<Guid> pasosSiguientesAgregados = new List<Guid>();
            if (idsPasosSiguientes != null && idsPasosSiguientes.Count > 0)
            {
                foreach (var idPasoSiguiente in idsPasosSiguientes)
                {
                    if(! await _db.PasosSiguientes.AnyAsync(ps => ps.IdPaso == idPaso && ps.IdPasoSiguiente == idPasoSiguiente)) { 
                        var pasoSiguienteEntity = new PasoSiguienteEntity
                        {
                            IdPaso = idPaso,
                            IdPasoSiguiente = idPasoSiguiente
                        };
                        await _db.PasosSiguientes.AddAsync(pasoSiguienteEntity);
                        pasosSiguientesAgregados.Add(idPasoSiguiente);
                    }
                }
            }
            await _db.SaveAsync();
            return pasosSiguientesAgregados;
        }
    }
}
