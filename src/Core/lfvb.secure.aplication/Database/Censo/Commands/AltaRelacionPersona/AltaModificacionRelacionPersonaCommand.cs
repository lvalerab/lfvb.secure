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

        public async Task<RelacionPersonaModel> execute(RelacionPersonaModel model)
        {
            
            //Comprobamos los datos
            if(model == null)   
                throw new ArgumentNullException(nameof(model));

            if(model.Tipo == null)
                throw new ArgumentNullException(nameof(model.Tipo));

            if(model.Persona1 == null)  
                throw new ArgumentNullException(nameof(model.Persona1));
            if(model.Persona2 == null)
                throw new ArgumentNullException(nameof(model.Persona2));

            TipoRelacionPersonaEntity? tipoRelacion = await (from tr in _db.TiposRelacionesPersona.Include(p=>p.TipoReciploco)
                                                            where tr.Codigo == model.Tipo.Codigo
                                                            select tr).FirstOrDefaultAsync();

            //Buscamos si existe la relacion
            RelacionPersonaEntity? rel=await (from rle in _db.RelacionesPersona
                                            where rle.IdPersona == model.Persona1.Id 
                                               && rle.IdPersonaRelacionada == model.Persona2.Id 
                                               && rle.CodigoTipoRelacion == model.Tipo.Codigo
                                            select rle).FirstOrDefaultAsync();
            if(rel == null)
            {
                //La agregamos
                rel = new RelacionPersonaEntity
                {
                    IdPersona = model.Persona1.Id??Guid.Empty,
                    IdPersonaRelacionada = model.Persona2.Id??Guid.Empty,
                    CodigoTipoRelacion = model.Tipo.Codigo,
                    InicioVigencia = model.FechaInicioVigencia ?? DateTime.Now,
                    FinVigencia = model.FechaFinVigencia,
                    Observaciones = model.Observaciones
                };
                _db.RelacionesPersona.Add(rel);
                if(tipoRelacion.CodigoReciproco != null)
                {
                    //Agregamos la relacion reciproca
                    RelacionPersonaEntity relReciproca = new RelacionPersonaEntity
                    {
                        IdPersona = model.Persona2.Id ?? Guid.Empty,
                        IdPersonaRelacionada = model.Persona1.Id ?? Guid.Empty,
                        CodigoTipoRelacion = tipoRelacion.CodigoReciproco,
                        InicioVigencia = model.FechaInicioVigencia ?? DateTime.Now,
                        FinVigencia = model.FechaFinVigencia,
                        Observaciones = model.Observaciones
                    };
                    _db.RelacionesPersona.Add(relReciproca);
                }
            } else
            {
                //Actualizamos las observaciones y fechas
                rel.Observaciones = model.Observaciones;
                rel.InicioVigencia = model.FechaInicioVigencia ?? rel.InicioVigencia;
                rel.FinVigencia = model.FechaFinVigencia ?? rel.FinVigencia;
                _db.RelacionesPersona.Update(rel);

                //Buscamos la relacion reciproca
                if (tipoRelacion.CodigoReciproco != null)
                {
                    RelacionPersonaEntity? relReciproca = await (from rle in _db.RelacionesPersona
                                                                where rle.IdPersona == model.Persona2.Id
                                                                   && rle.IdPersonaRelacionada == model.Persona1.Id
                                                                   && rle.CodigoTipoRelacion == tipoRelacion.CodigoReciproco
                                                                select rle).FirstOrDefaultAsync();
                    if(relReciproca != null)
                    {
                        relReciproca.Observaciones = model.Observaciones;
                        relReciproca.InicioVigencia = model.FechaInicioVigencia ?? relReciproca.InicioVigencia;
                        relReciproca.FinVigencia = model.FechaFinVigencia ?? relReciproca.FinVigencia;
                        _db.RelacionesPersona.Update(relReciproca);
                    } else
                    {
                        //Agregamos la relacion reciproca
                        RelacionPersonaEntity relReciprocaNueva = new RelacionPersonaEntity
                        {
                            IdPersona = model.Persona2.Id ?? Guid.Empty,
                            IdPersonaRelacionada = model.Persona1.Id ?? Guid.Empty,
                            CodigoTipoRelacion = tipoRelacion.CodigoReciproco,
                            InicioVigencia = model.FechaInicioVigencia ?? DateTime.Now,
                            FinVigencia = model.FechaFinVigencia,
                            Observaciones = model.Observaciones
                        };
                        _db.RelacionesPersona.Add(relReciprocaNueva);
                    }
                }
            }

            await _db.SaveAsync();

            return model;
        }
    }
}
