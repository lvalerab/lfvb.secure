using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetRelaciones
{
    public class GetRelacionesPersonaQuery: IGetRelacionesPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetRelacionesPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<RelacionPersonaModel>> execute(Guid idPersona)
        {
            List<RelacionPersonaModel> relaciones= await (from rel in _db.RelacionesPersona
                                                                            .Include(r=>r.Persona).ThenInclude(p=>p.TipoPersona) 
                                                                            .Include(r=>r.PersonaRelacionada).ThenInclude(p=>p.TipoPersona)
                                                                            .Include(p=>p.TipoRelacionPersona)                                                                            
                                                           where rel.IdPersona == idPersona
                                                           select new RelacionPersonaModel
                                                           {
                                                               
                                                               Tipo = new TipoRelacionPersonaModel
                                                               {
                                                                   Codigo = rel.TipoRelacionPersona.Codigo,
                                                                   Nombre = rel.TipoRelacionPersona.Nombre
                                                               },
                                                               Persona1= new PersonaModel
                                                               {
                                                                   Id = rel.Persona.Id,
                                                                   Tipo = new TipoPersonaModel
                                                                   {
                                                                       Codigo = rel.Persona.TipoPersona.Codigo,
                                                                       Nombre = rel.Persona.TipoPersona.Nombre
                                                                   },
                                                                   Nombre = rel.Persona.Nombre,
                                                                   Apellido1 = rel.Persona.Apellido1,
                                                                   Apellido2 = rel.Persona.Apellido2,
                                                                   FechaNacimiento = rel.Persona.FechaNacimiento
                                                               },
                                                               Persona2 = new PersonaModel
                                                               {
                                                                   Id = rel.PersonaRelacionada.Id,
                                                                   Tipo = new TipoPersonaModel
                                                                    {
                                                                        Codigo = rel.PersonaRelacionada.TipoPersona.Codigo,
                                                                        Nombre = rel.PersonaRelacionada.TipoPersona.Nombre
                                                                    },
                                                                   Nombre = rel.PersonaRelacionada.Nombre,
                                                                   Apellido1 = rel.PersonaRelacionada.Apellido1,
                                                                   Apellido2 = rel.PersonaRelacionada.Apellido2,
                                                                   FechaNacimiento = rel.PersonaRelacionada.FechaNacimiento
                                                               },
                                                               FechaInicioVigencia = rel.InicioVigencia,
                                                               FechaFinVigencia = rel.FinVigencia,
                                                           }).ToListAsync();
            return relaciones;
        }
    }
}
