using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.AltaRelacionPersona
{
    public class AltaModificacionRelacionPersonaCommand: IAltaModificacionRelacionPersonaCommand
    {
        private readonly IDataBaseService _db;

        public AltaModificacionRelacionPersonaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<RelacionPersonaModel> execute(AltaModificacionRelacionPersonalModel model)
        {
            
            //Comprobamos los datos
            if(model == null)   
                throw new ArgumentNullException(nameof(model));

            if(model.Tipo == null)
                throw new ArgumentNullException(nameof(model.Tipo));

            if(model.IdPersona1 == null)  
                throw new ArgumentNullException(nameof(model.IdPersona1));
            if(model.IdPersona2 == null)
                throw new ArgumentNullException(nameof(model.IdPersona2));

            TipoRelacionPersonaEntity? tipoRelacion = await (from tr in _db.TiposRelacionesPersona.Include(p=>p.TipoReciploco)
                                                            where tr.Codigo == model.Tipo.Codigo
                                                            select tr).FirstOrDefaultAsync();

            //Buscamos si existe la relacion
            RelacionPersonaEntity? rel=await (from rle in _db.RelacionesPersona
                                            where rle.IdPersona ==model.IdPersona1 
                                               && rle.IdPersonaRelacionada == model.IdPersona2
                                               && rle.CodigoTipoRelacion == model.Tipo.Codigo
                                            select rle).FirstOrDefaultAsync();
            if(rel == null)
            {
                //La agregamos
                rel = new RelacionPersonaEntity
                {
                    IdPersona = model.IdPersona1??Guid.Empty,
                    IdPersonaRelacionada = model.IdPersona2??Guid.Empty,
                    CodigoTipoRelacion = model.Tipo.Codigo,
                    InicioVigencia = model.FechaInicio ?? DateTime.Now,
                    FinVigencia = model.FechaFin,
                    Observaciones = (model.Observaciones?.Trim() != "") ? DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n------------------------------------------\n" + model.Observaciones?.Trim() : ""
                };
                _db.RelacionesPersona.Add(rel);
                if(tipoRelacion.CodigoReciproco != null)
                {
                    //Buscamos si existe la relacion
                    RelacionPersonaEntity? relReci = await (from rle in _db.RelacionesPersona
                                                        where rle.IdPersona == model.IdPersona2
                                                           && rle.IdPersonaRelacionada == model.IdPersona1
                                                           && rle.CodigoTipoRelacion == model.Tipo.Codigo
                                                        select rle).FirstOrDefaultAsync();
                    if(relReci==null) { 
                        //Agregamos la relacion reciproca
                        RelacionPersonaEntity relReciproca = new RelacionPersonaEntity
                        {
                            IdPersona = model.IdPersona2 ?? Guid.Empty,
                            IdPersonaRelacionada = model.IdPersona1 ?? Guid.Empty,
                            CodigoTipoRelacion = tipoRelacion.CodigoReciproco,
                            InicioVigencia = model.FechaInicio ?? DateTime.Now,
                            FinVigencia = model.FechaFin,
                            Observaciones = (model.Observaciones?.Trim() != "") ? DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n------------------------------------------\n"+ model.Observaciones?.Trim() : ""
                        };
                        _db.RelacionesPersona.Add(relReciproca);
                    } else
                    {
                        //Actualizamos las observaciones y fechas
                        relReci.Observaciones = relReci.Observaciones + "\n------------------------------------------\n" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n------------------------------------------\n" + (model.Observaciones ?? "");
                        relReci.InicioVigencia = model.FechaInicio ?? relReci.InicioVigencia;
                        relReci.FinVigencia = model.FechaFin ?? relReci.FinVigencia;
                        _db.RelacionesPersona.Update(relReci);
                    }
                }
            } else
            {
                //Actualizamos las observaciones y fechas
                rel.Observaciones = rel.Observaciones+ "\n------------------------------------------\n" + DateTime.Now.ToString("dd/MM/yyyy HH:mm")+"\n------------------------------------------\n"+ (model.Observaciones??"");
                rel.InicioVigencia = model.FechaInicio ?? rel.InicioVigencia;
                rel.FinVigencia = model.FechaFin ?? rel.FinVigencia;
                _db.RelacionesPersona.Update(rel);

                //Buscamos la relacion reciproca
                if (tipoRelacion.CodigoReciproco != null)
                {
                    RelacionPersonaEntity? relReciproca = await (from rle in _db.RelacionesPersona
                                                                where rle.IdPersona == model.IdPersona2
                                                                   && rle.IdPersonaRelacionada == model.IdPersona1
                                                                   && rle.CodigoTipoRelacion == tipoRelacion.CodigoReciproco
                                                                select rle).FirstOrDefaultAsync();
                    if(relReciproca != null)
                    {
                        relReciproca.Observaciones = model.Observaciones;
                        relReciproca.InicioVigencia = model.FechaInicio ?? relReciproca.InicioVigencia;
                        relReciproca.FinVigencia = model.FechaFin ?? relReciproca.FinVigencia;
                        _db.RelacionesPersona.Update(relReciproca);
                    } else
                    {
                        //Agregamos la relacion reciproca
                        RelacionPersonaEntity relReciprocaNueva = new RelacionPersonaEntity
                        {
                            IdPersona = model.IdPersona2 ?? Guid.Empty,
                            IdPersonaRelacionada = model.IdPersona1 ?? Guid.Empty,
                            CodigoTipoRelacion = tipoRelacion.CodigoReciproco,
                            InicioVigencia = model.FechaInicio ?? DateTime.Now,
                            FinVigencia = model.FechaFin,
                            Observaciones = model.Observaciones?.Trim()??""
                        };
                        _db.RelacionesPersona.Add(relReciprocaNueva);
                    }
                }
            }

            await _db.SaveAsync();

            RelacionPersonaModel rela = await (from rl in _db.RelacionesPersona.Include(r => r.Persona).ThenInclude(p => p.TipoPersona)
                                                                                   .Include(r => r.PersonaRelacionada).ThenInclude(p => p.TipoPersona)
                                                                                   .Include(r => r.TipoRelacionPersona)
                                                    where rl.Persona.Id == model.IdPersona1
                                                      && rl.PersonaRelacionada.Id == model.IdPersona2
                                                      && rl.TipoRelacionPersona.Codigo == model.Tipo.Codigo
                                                    select new RelacionPersonaModel
                                                    {
                                                        Persona1=new PersonaModel
                                                        {
                                                            Id=rl.Persona.Id,
                                                            Nombre=rl.Persona.Nombre,
                                                            Apellido1=rl.Persona.Apellido1,
                                                            Apellido2=rl.Persona.Apellido2,
                                                            FechaNacimiento=rl.Persona.FechaNacimiento,
                                                            Identificaciones=new List<IdentificacionPersonaModel>(),
                                                            Relaciones=new List<RelacionPersonaModel>(),
                                                            Sexo=(from sx in _db.TiposSexoPersona
                                                                  where sx.Codigo==rl.Persona.CodigoSexo
                                                                  select new TipoSexoPersonaModel
                                                                  {
                                                                      Codigo=sx.Codigo,
                                                                      Nombre=sx.Nombre
                                                                  }).FirstOrDefault(),
                                                            Situaciones =new List<SituacionPersonaModel>(),
                                                            Tipo=new TipoPersonaModel
                                                            {
                                                                Codigo=rl.Persona.TipoPersona.Codigo,
                                                                Nombre=rl.Persona.TipoPersona.Nombre
                                                            }
                                                        },
                                                        Persona2= new PersonaModel
                                                        {
                                                            Id = rl.PersonaRelacionada.Id,
                                                            Nombre = rl.PersonaRelacionada.Nombre,
                                                            Apellido1 = rl.PersonaRelacionada.Apellido1,
                                                            Apellido2 = rl.PersonaRelacionada.Apellido2,
                                                            FechaNacimiento = rl.PersonaRelacionada.FechaNacimiento,
                                                            Identificaciones = new List<IdentificacionPersonaModel>(),
                                                            Relaciones = new List<RelacionPersonaModel>(),
                                                            Sexo = (from sx in _db.TiposSexoPersona
                                                                    where sx.Codigo == rl.PersonaRelacionada.CodigoSexo
                                                                    select new TipoSexoPersonaModel
                                                                    {
                                                                        Codigo = sx.Codigo,
                                                                        Nombre = sx.Nombre
                                                                    }).FirstOrDefault(),
                                                            Situaciones = new List<SituacionPersonaModel>(),
                                                            Tipo = new TipoPersonaModel
                                                            {
                                                                Codigo = rl.PersonaRelacionada.TipoPersona.Codigo,
                                                                Nombre = rl.PersonaRelacionada.TipoPersona.Nombre
                                                            }
                                                        },
                                                        FechaInicioVigencia=rl.InicioVigencia,
                                                        FechaFinVigencia=rl.FinVigencia,
                                                        Observaciones=rl.Observaciones,
                                                        Tipo=new TipoRelacionPersonaModel
                                                        {
                                                            Codigo=rl.TipoRelacionPersona.Codigo,
                                                            Nombre=rl.TipoRelacionPersona.Nombre
                                                        }
                                                    }).FirstOrDefaultAsync();

            return rela;
        }
    }
}
