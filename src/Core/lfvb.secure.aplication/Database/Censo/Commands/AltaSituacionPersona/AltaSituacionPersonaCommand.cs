using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.AltaSituacionPersona
{
    public class AltaSituacionPersonaCommand: IAltaSituacionPersonaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaSituacionPersonaCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento; 
        }

        public async Task<SituacionPersonaModel> execute(SituacionPersonaModel model)
        {

            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if(model.Persona == null || model.Tipo == null)
            {
                throw new ArgumentException("La persona y el tipo de situación no pueden ser nulos.");
            }   

            if(model.Id != null && model.Id != Guid.Empty)
            {
                throw new ArgumentException("El ID debe ser nulo o vacío para una nueva situación de persona.");
            }

            model.Id = await _cmdAltaElemento.execute("sipe",false);   
            
            SituacionPersonaEntity entity = new SituacionPersonaEntity
            {
                Id = model.Id.Value,
                CodigoSituacion = model.Tipo.Codigo,
                IdPersona = model.Persona.Id.Value,
                FechaDesde = model.FechaInicio??DateTime.Now,
                FechaHasta = model.FechaFin,
                Observaciones = model.Observaciones
            };

            _db.SituacionesPersona.Add(entity);

            await _db.SaveAsync();
            // Implementación del comando
            return model;
        }
    }
}
