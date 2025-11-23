using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public class ModificarPasoCircuitoCommand : IModificarPasoCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public ModificarPasoCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<PasoModel> execute(PasoModel paso)
        {

            if(paso.Id == null)
                throw new ArgumentNullException("El Id del paso no puede ser nulo");    

            PasoEntity? pasoEntity = await (from p in _db.Pasos
                                          where p.Id == paso.Id
                                          select p).FirstOrDefaultAsync();

            if (pasoEntity == null)
            {
                return null;
            } else
            {
                pasoEntity.CodEstado = paso.Estado != null ? paso.Estado.Codigo : pasoEntity.CodEstado;
                pasoEntity.CodEstadoSiguiente = paso.EstadoNuevo != null ? paso.EstadoNuevo.Codigo : pasoEntity.CodEstadoSiguiente;
                pasoEntity.IdCircuitoSiguiente = paso.CircuitoSiguiente != null ? paso.CircuitoSiguiente.Id : pasoEntity.IdCircuitoSiguiente;
                pasoEntity.Nombre = paso.Nombre;
                await _db.SaveAsync();
            }
            return paso;
        }
    }
}
