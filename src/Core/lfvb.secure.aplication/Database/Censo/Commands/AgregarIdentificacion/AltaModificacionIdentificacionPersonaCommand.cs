using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.AgregarIdentificacion
{
    public class AltaModificacionIdentificacionPersonaCommand: IAltaModificacionIdentificacionPersonaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaModificacionIdentificacionPersonaCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento;
        }

        public async Task<IdentificacionPersonaModel> execute(IdentificacionPersonaModel identificacion, bool commit = true)
        {
            if (identificacion == null)
            {
                throw new ArgumentNullException(nameof(identificacion));
            }

            if (identificacion.Persona == null || identificacion.Persona.Id == null || identificacion.Persona.Id == Guid.Empty)
            {
                throw new ArgumentNullException("Tiene que indicar una persona a la que se le asignara la identificacion");
            }

            if (identificacion.Tipo == null)
            {
                throw new ArgumentNullException("Tiene que indicar un tipo de identificacion");
            }

            if (string.IsNullOrEmpty(identificacion.Dato1) && string.IsNullOrEmpty(identificacion.Dato2))
            {
                throw new ArgumentNullException("Tiene que indicar un dato de identificacion");
            }

            if (identificacion.Id != null && identificacion.Id != Guid.Empty)
            {
                throw new ArgumentException("No se puede indicar un id para la identificacion, este se asignara automaticamente");
            }

            //Buscamos si existe una identificacion vigente del mismo tipo para la persona
            var identificacionVigente = await (from ide in _db.IdentificadoresPersona
                                               where ide.Persona.Id == identificacion.Persona.Id
                                              && ide.TipoIdentificadorPersona.Codigo == identificacion.Tipo.Codigo
                                              && (ide.InicioVigencia <= DateTime.Now)
                                              && (ide.FinVigencia == null || ide.FinVigencia > DateTime.Now)
                                               select ide).FirstOrDefaultAsync();
            if (identificacionVigente != null)
            {
                //Si existe una identificacion vigente del mismo tipo para la persona, se le asigna la fecha de fin de vigencia a la fecha actual
                identificacionVigente.Dato1 = identificacion.Dato1;
                identificacionVigente.Dato2 = identificacion.Dato2;
                identificacionVigente.InicioVigencia = identificacion.FechaInicioVigencia ?? DateTime.Now;
                identificacionVigente.FinVigencia = identificacion.FechaFinVigencia;
                _db.IdentificadoresPersona.Update(identificacionVigente);
            }
            else
            {
                identificacion.Id = await _cmdAltaElemento.execute("tiid",false);
                //Agregamos la nueva identificacion
                IdentificadorPersonaEntity nueva = new IdentificadorPersonaEntity
                {
                    Id = Guid.NewGuid(),
                    IdPersona = identificacion.Persona.Id.Value,
                    CodigoTipoIdentificador = identificacion.Tipo.Codigo,
                    Dato1 = identificacion.Dato1,
                    Dato2 = identificacion.Dato2,
                    InicioVigencia = identificacion.FechaInicioVigencia ?? DateTime.Now,
                    FinVigencia = identificacion.FechaFinVigencia
                };
            }
            if (commit)
            {
                await _db.SaveAsync();
            }
            return identificacion;
        }
    }
}
