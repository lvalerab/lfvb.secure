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

        private FiltroBusquedaPersonasModel AplicarTipoCruce(TipoCrucePersonas cruce, FiltroBusquedaPersonasModel filtro)
        {
            FiltroBusquedaPersonasModel filtroAplicable=new FiltroBusquedaPersonasModel
            {
                Id = filtro.Id,
                Nombre = filtro.Nombre?.ToUpper(),
                Apellido1 = filtro.Apellido1?.ToUpper(),
                Apellido2 = filtro.Apellido2?.ToUpper(),
                Identificaciones = filtro.Identificaciones
            };
            if(filtroAplicable.Identificaciones != null && filtroAplicable.Identificaciones.Count>=0) { 
                for (int i=0; i<filtroAplicable.Identificaciones?.Count; i++)
                {
                    filtroAplicable.Identificaciones[i].Dato1 = filtroAplicable.Identificaciones[i].Dato1?.ToUpper();
                    filtroAplicable.Identificaciones[i].Dato2 = filtroAplicable.Identificaciones[i].Dato2?.ToUpper();
                }
            }

            switch (cruce)
            {
                case TipoCrucePersonas.Exacto:
                    break;
                case TipoCrucePersonas.Similar:
                    if (!string.IsNullOrEmpty(filtro.Nombre))
                        filtroAplicable.Nombre = filtro.Nombre.Substring(0, Math.Min(4, filtro.Nombre.Length));
                    if (!string.IsNullOrEmpty(filtro.Apellido1))
                        filtroAplicable.Apellido1 = filtro.Apellido1.Substring(0, Math.Min(4, filtro.Apellido1.Length));
                    if (!string.IsNullOrEmpty(filtro.Apellido2))
                        filtroAplicable.Apellido2 = filtro.Apellido2.Substring(0, Math.Min(4, filtro.Apellido2.Length));
                    break;
                case TipoCrucePersonas.SoloApellidosNombre:
                    filtroAplicable.Id = null;
                    filtro.Nombre= null;
                    filtro.Identificaciones = null;
                    break;
                case TipoCrucePersonas.SoloIdentificadores:
                    filtro.Nombre=null;
                    filtro.Apellido1=null;
                    filtro.Apellido2=null;  
                    break;
            }

            return filtro;
        }

        public async Task<List<PersonaModel>> execute(FiltroBusquedaPersonasModel filtro)
        {
            return await execute(filtro, TipoCrucePersonas.Exacto);
        }

        public async Task<List<PersonaModel>> execute(FiltroBusquedaPersonasModel filtro, TipoCrucePersonas cruce=TipoCrucePersonas.Exacto)
        {
            filtro = AplicarTipoCruce(cruce, filtro);

            List<PersonaModel> personas = await (from pr in _db.Personas.Include(p=>p.TipoPersona)
                                                                        .Include(p=>p.TipoSexo)  
                                                                        .Include(p=>p.Identificadores).ThenInclude(i=>i.TipoIdentificadorPersona)                                                                        
                                                 where (filtro.Id == null || pr.Id == filtro.Id) &&
                                                      (filtro.Nombre == null || pr.Nombre.ToUpper().Contains(filtro.Nombre.ToUpper())) &&
                                                      (filtro.Apellido1 == null || pr.Apellido1.ToUpper().Contains(filtro.Apellido1.ToUpper())) &&
                                                      (filtro.Apellido2 == null || pr.Apellido2.ToUpper().Contains(filtro.Apellido2.ToUpper())) &&
                                                      (filtro.Identificaciones.Count == 0 || pr.Identificadores.Any(i => filtro.Identificaciones.Any(fi => fi.Tipo.Codigo == i.TipoIdentificadorPersona.Codigo && 
                                                                                                                                                                        (
                                                                                                                                                                            (fi.Dato1!= null && i.Dato1.Contains(fi.Dato1)) 
                                                                                                                                                                                &&
                                                                                                                                                                            (fi.Dato2 != null && i.Dato2.Contains(fi.Dato2))
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
                                                    }).ToList()
                                                }).ToListAsync();
            return personas;
        }
    }
}
