using lfvb.secure.aplication.Database.Censo.Commands.AgregarIdentificacion;
using lfvb.secure.aplication.Database.Censo.Exceptions;
using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.AltaPersona
{
    public class AltaPersonaCommand: IAltaPersonaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IBuscadorPersonaQuery _qryBuscador;
        private readonly IAltaElementoCommand _cmdAltaElemento;
        private readonly IAltaModificacionIdentificacionPersonaCommand _cmdAltaModiIdent;

        public AltaPersonaCommand(IDataBaseService db,
            IBuscadorPersonaQuery qryBuscador,
            IAltaElementoCommand cmdAltaElemento,
            IAltaModificacionIdentificacionPersonaCommand cmdAltaModiIdent
            )
        {
            _db = db;
            _qryBuscador = qryBuscador;
            _cmdAltaElemento = cmdAltaElemento;
            _cmdAltaModiIdent = cmdAltaModiIdent;
        }   

        public async Task<PersonaModel> execute(PersonaModel persona, bool SinComprobarBusqueda=false)
        {
            if(!SinComprobarBusqueda)
            {
                //Intentamos buscar a la persona por los varios filtros, por si acaso, si existe, no la damos de alta, y devolvemos una excepcion de persona encontrada
                FiltroBusquedaPersonasModel filtro = new FiltroBusquedaPersonasModel
                {
                    Nombre = persona.Nombre,
                    Apellido1 = persona.Apellido1,
                    Apellido2 = persona.Apellido2,
                    Identificaciones = new List<FiltroBusquedaPorIdentPersona>()
                };
            
                if (persona.Identificaciones != null)
                {
                    foreach (var ident in persona.Identificaciones)
                    {
                        filtro.Identificaciones.Add(new FiltroBusquedaPorIdentPersona
                        {
                            Tipo = ident.Tipo,
                            Dato1 = ident.Dato1,
                            Dato2 = ident.Dato2
                        });
                    }
                }

                List<PersonaModel> personasEncontradas = await _qryBuscador.execute(filtro,TipoCrucePersonas.Exacto);
                if(personasEncontradas == null && personasEncontradas.Count <= 0)
                {
                    personasEncontradas = await _qryBuscador.execute(filtro, TipoCrucePersonas.Similar);
                }
                if(personasEncontradas != null && personasEncontradas.Count > 0)
                {
                    throw new PersonaEncontradaEnAltaException("Persona encontrada con los datos proporcionados, no se puede dar de alta", personasEncontradas);
                }
            }
            if(persona.Id != null && persona.Id != Guid.Empty)
            {
                throw new PersonaEncontradaEnAltaException("La persona indicada ya tiene identificador propio, para modificarla use la opcion de actualizar", new List<PersonaModel> { persona });
            }
            //Si no se encuentra a la persona, o se ha indicado que no se compruebe, se da de alta
            persona.Id = await _cmdAltaElemento.execute("pers",false);
            PersonaEntity personaEntity = new PersonaEntity
            {
                Id = persona.Id.Value,
                Nombre = persona.Nombre??"",
                Apellido1 = persona.Apellido1??"",
                Apellido2 = persona.Apellido2??"",
                CodigoSexo = persona.Sexo != null ? persona.Sexo.Codigo : "NB",
                FechaNacimiento = persona.FechaNacimiento,
                CodigoTipoPersona = persona.Tipo != null ? persona.Tipo.Codigo : "F"
            };

            await _db.Personas.AddAsync(personaEntity);

            //Agregamos las identificaciones
            List<IdentificacionPersonaModel> identificaciones = new List<IdentificacionPersonaModel>(); 
            if (persona.Identificaciones != null)
            {
                foreach (var ident in persona.Identificaciones)
                {
                    ident.Persona = persona;
                    identificaciones.Add(await _cmdAltaModiIdent.execute(ident, false));
                    ident.Persona = null;
                }
            }
            persona.Identificaciones = identificaciones;

            await _db.SaveAsync();

            return persona;
        }
    }
}
