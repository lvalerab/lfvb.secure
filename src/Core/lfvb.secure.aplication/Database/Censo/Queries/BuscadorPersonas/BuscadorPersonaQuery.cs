using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas
{
    public class BuscadorPersonaQuery: IBuscadorPersonaQuery
    {
        private readonly IDataBaseService _db;  
        public BuscadorPersonaQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<PersonaModel>> execute(FiltroBusquedaPersonasModel filtro)
        {
            List<PersonaModel> personas = await (from pr in _db.Personas.Include(p=>p.TipoPersona)
                                                                        .Include(p=>p.Identificadores).ThenInclude(i=>i.TipoIdentificadorPersona)                                                                        
                                                 where (filtro.Id == null || pr.Id == filtro.Id) &&
                                                      (filtro.Nombre == null || pr.Nombre.ToUpper().Contains(filtro.Nombre.ToUpper())) &&
                                                      (filtro.Apellido1 == null || pr.Apellido1.ToUpper().Contains(filtro.Apellido1.ToUpper())) &&
                                                      (filtro.Apellido2 == null || pr.Apellido2.ToUpper().Contains(filtro.Apellido2.ToUpper())) &&
                                                      (filtro.Identificaciones.Count == 0 || pr.Identificadores.Any(i => filtro.Identificaciones.Any(fi => fi.Tipo.Codigo == i.TipoIdentificadorPersona.Codigo && 
                                                                                                                                                                        (
                                                                                                                                                                            (fi.Dato1!= null && fi.Dato1 == i.Dato1) 
                                                                                                                                                                                &&
                                                                                                                                                                            (fi.Dato2 != null && fi.Dato2 == i.Dato2)
                                                                                                                                                                        )
                                                                                                                                                     )
                                                                                                                    )
                                                      )
                                                select new PersonaModel
                                                {
                                                    Id = pr.Id,
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
                                                        Tipo = new TipoIdentificacionPersonaModel
                                                        {
                                                            Codigo = i.TipoIdentificadorPersona.Codigo,
                                                            Nombre = i.TipoIdentificadorPersona.Nombre
                                                        },
                                                        Dato1 = i.Dato1,
                                                        Dato2 = i.Dato2,
                                                        FechaInicioVigencia = i.InicioVigencia,
                                                        FechaFinVigencia = i.FinVigencia
                                                    }).ToList()
                                                }).ToListAsync();
            return personas;
        }
    }
}
