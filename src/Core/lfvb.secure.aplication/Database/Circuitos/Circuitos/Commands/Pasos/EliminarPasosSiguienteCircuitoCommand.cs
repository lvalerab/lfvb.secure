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
    public class EliminarPasosSiguienteCircuitoCommand : IEliminarPasosSiguienteCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public EliminarPasosSiguienteCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<Guid>> execute(Guid idPaso, List<Guid> idsPasosSiguientes)
        {
            List<Guid> pasosSiguientesEliminados = new List<Guid>();
            if (idsPasosSiguientes != null && idsPasosSiguientes.Count > 0)
            {
                foreach (var idPasoSiguiente in idsPasosSiguientes)
                {
                  
                    var pasoSiguienteEntity = new PasoSiguienteEntity
                    {
                        IdPaso = idPaso,
                        IdPasoSiguiente = idPasoSiguiente
                    };
                    var encontrado= await _db.PasosSiguientes.FirstOrDefaultAsync(ps => ps.IdPaso == idPaso && ps.IdPasoSiguiente == idPasoSiguiente);
                    if (encontrado != null) { 
                        _db.PasosSiguientes.Remove(encontrado);
                        pasosSiguientesEliminados.Add(idPasoSiguiente);
                    }
                  
                }
            }
            await _db.SaveAsync();
            return pasosSiguientesEliminados;
        }
    }
}
