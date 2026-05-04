using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.GetPersona
{
    public class GetPersonaQuery : IGetPersonaQuery
    {
        private readonly IDataBaseService _db;

        public GetPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }


        public async Task<PersonaModel> execute(Guid id)
        {
            PersonaModel resultado = await (from pr in _db.Personas
                                            .Include(p => p.TipoSexo)   
                                            .Include(p => p.Identificadores).ThenInclude(i => i.TipoIdentificadorPersona)  
                                            .Include(p => p.Relaciones).ThenInclude(r => r.PersonaRelacionada)    
                                            .Include(p => p.TipoPersona)
                                            .Include(p => p.Situaciones).ThenInclude(s=>s.TipoSituacionPersona)   
                                            where pr.Id == id
                                            select new PersonaModel
                                            {
                                                Id=pr.Id,
                                                Tipo = new TipoPersonaModel
                                                {
                                                    Codigo = pr.TipoPersona.Codigo,
                                                    Nombre = pr.TipoPersona.Nombre
                                                },
                                                Sexo = new TipoSexoPersonaModel
                                                {
                                                    Codigo = pr.TipoSexo.Codigo,
                                                    Nombre = pr.TipoSexo.Nombre
                                                },  
                                                Nombre = pr.Nombre,
                                                Apellido1 = pr.Apellido1,
                                                Apellido2 = pr.Apellido2,
                                                FechaNacimiento = pr.FechaNacimiento,
                                                Identificaciones = pr.Identificadores.Select(i => new IdentificacionPersonaModel
                                                {
                                                    Id = i.Id,                                                    
                                                    Tipo = new TipoIdentificacionPersonaModel
                                                    {
                                                        Codigo = i.TipoIdentificadorPersona.Codigo,
                                                        Nombre = i.TipoIdentificadorPersona.Nombre  
                                                    },
                                                    Dato1 = i.Dato1,
                                                    Dato2 = i.Dato2,
                                                    FechaInicioVigencia = i.InicioVigencia,
                                                    FechaFinVigencia = i.FinVigencia
                                                }).ToList(),
                                                Relaciones = pr.Relaciones.Select(r => new RelacionPersonaModel
                                                {
                                                    Persona2 = new PersonaModel
                                                    {
                                                        Id = r.PersonaRelacionada.Id,
                                                        Nombre = r.PersonaRelacionada.Nombre,
                                                        Apellido1 = r.PersonaRelacionada.Apellido1,
                                                        Apellido2 = r.PersonaRelacionada.Apellido2
                                                    },
                                                    Tipo = (from tr in _db.TiposRelacionesPersona
                                                                    where tr.Codigo == r.CodigoTipoRelacion
                                                                    select new TipoRelacionPersonaModel
                                                                    {
                                                                        Codigo = tr.Codigo,
                                                                        Nombre = tr.Nombre
                                                                    }).FirstOrDefault()
                                                }).ToList(),
                                                Situaciones = pr.Situaciones.Select(s => new SituacionPersonaModel
                                                {
                                                    Id = s.Id,
                                                    Tipo = new TipoSituacionPersonaModel
                                                    {
                                                        Codigo = s.TipoSituacionPersona.Codigo,
                                                        Nombre = s.TipoSituacionPersona.Nombre
                                                    },
                                                    Observaciones = s.Observaciones,
                                                    FechaInicio = s.FechaDesde,
                                                    FechaFin = s.FechaHasta
                                                }).ToList()
                                            }).FirstOrDefaultAsync();

            return resultado;
        }
    }
}
