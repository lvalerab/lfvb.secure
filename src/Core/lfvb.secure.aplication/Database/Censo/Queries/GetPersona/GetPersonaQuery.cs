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
                                            .Include(p => p.Identificadores)                                            
                                            .Include(p => p.Relaciones)
                                            .Include(p => p.TipoPersona)
                                            where pr.Id == id
                                            select new PersonaModel
                                            {
                                                Id=pr.Id,
                                                Tipo = new TipoPersonaModel
                                                {
                                                    Codigo = pr.TipoPersona.Codigo,
                                                    Nombre = pr.TipoPersona.Nombre
                                                },
                                                Nombre = pr.Nombre,
                                                Apellido1 = pr.Apellido1,
                                                Apellido2 = pr.Apellido2,
                                                Identificaciones = pr.Identificadores.Select(i => new IdentificacionPersonaModel
                                                {
                                                    Id = i.Id,                                                    
                                                    Tipo = (from ti in _db.TiposIdentificadoresPersona
                                                                          where ti.Codigo==i.CodigoTipoIdentificador
                                                                          select new TipoIdentificacionPersonaModel
                                                                          {
                                                                                Codigo = ti.Codigo,
                                                                                Nombre = ti.Nombre
                                                                          }).FirstOrDefault(),
                                                    Dato1 = i.Dato1,
                                                    Dato2 = i.Dato2,
                                                    FechaInicioVigencia = i.InicioVigencia,
                                                    FechaFinVigencia = i.FinVigencia
                                                }).ToList(),
                                                Relaciones = pr.Relaciones.Select(r => new RelacionPersonaModel
                                                {
                                                    Persona2 = (from prr in _db.Personas
                                                                          where prr.Id == r.IdPersonaRelacionada
                                                                          select new PersonaModel
                                                                          {
                                                                              Id = prr.Id,
                                                                              Tipo = new TipoPersonaModel
                                                                              {
                                                                                  Codigo = prr.TipoPersona.Codigo,
                                                                                  Nombre = prr.TipoPersona.Nombre
                                                                              },
                                                                              Nombre = prr.Nombre,
                                                                              Apellido1 = prr.Apellido1,
                                                                              Apellido2 = prr.Apellido2
                                                                          }).FirstOrDefault(),
                                                    Tipo = (from tr in _db.TiposRelacionesPersona
                                                                    where tr.Codigo == r.CodigoTipoRelacion
                                                                    select new TipoRelacionPersonaModel
                                                                    {
                                                                        Codigo = tr.Codigo,
                                                                        Nombre = tr.Nombre
                                                                    }).FirstOrDefault()
                                                }).ToList()
                                            }).FirstOrDefaultAsync();

            return resultado;
        }
    }
}
