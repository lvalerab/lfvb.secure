using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.ModificaPersona
{
    public class ModificarPersonaCommand: IModificarPersonaCommand
    {
        private readonly IDataBaseService _db;

        public ModificarPersonaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<PersonaModel> execute(PersonaModel persona)
        {
            if(persona.Id == null && persona.Id==Guid.Empty)
            {
                throw new Exception("El id de la persona no puede ser nulo");
            }

            PersonaEntity? pers=await (from p in _db.Personas
                                     where p.Id == persona.Id
                                     select p).FirstOrDefaultAsync();

            if (pers == null)
            {
                throw new Exception("Persona no encontrada");
            }
            else
            {
                pers.Nombre = persona.Nombre;
                pers.Apellido1 = persona.Apellido1;
                pers.Apellido2 = persona.Apellido2;
                pers.FechaNacimiento = persona.FechaNacimiento;

                _db.Personas.Update(pers);
                await _db.SaveAsync();
            }
            return persona;
        }
    }
}
