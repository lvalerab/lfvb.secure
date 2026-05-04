using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetIdentificadores
{
    public class GetIdentificadoresPersonaQuery : IGetIdentificadoresPersonaQuery
    {
        private readonly IDataBaseService _db;
        public GetIdentificadoresPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }
        public async Task<List<IdentificacionPersonaModel>> execute(Guid idPersona)
        {
            var result = await (from ip in _db.IdentificadoresPersona
                                            .Include(i => i.TipoIdentificadorPersona)
                                            .Include(i => i.Persona).ThenInclude(p => p.TipoPersona)
                                where ip.IdPersona == idPersona
                                select new IdentificacionPersonaModel
                                {
                                    Id = ip.Id,
                                    Tipo = new TipoIdentificacionPersonaModel
                                    {
                                        Codigo = ip.TipoIdentificadorPersona.Codigo,
                                        Nombre = ip.TipoIdentificadorPersona.Nombre
                                    },
                                    Dato1 = ip.Dato1,
                                    Dato2 = ip.Dato2,
                                    FechaInicioVigencia = ip.InicioVigencia,
                                    FechaFinVigencia = ip.FinVigencia,
                                    Persona = new PersonaModel
                                    {
                                        Id = ip.Persona.Id,
                                        Nombre = ip.Persona.Nombre,
                                        Apellido1 = ip.Persona.Apellido1,
                                        Apellido2 = ip.Persona.Apellido2,
                                        FechaNacimiento = ip.Persona.FechaNacimiento
                                    }
                                }).ToListAsync();
            return result;
        }
    }
}
