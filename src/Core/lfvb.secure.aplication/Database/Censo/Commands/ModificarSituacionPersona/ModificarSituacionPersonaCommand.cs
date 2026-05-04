using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.ModificarSituacionPersona
{
    public class ModificarSituacionPersonaCommand: IModificarSituacionPersonaCommand
    {
        private readonly IDataBaseService _db;

        public ModificarSituacionPersonaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<SituacionPersonaModel> execute(SituacionPersonaModel model)
        {
            //Buscamos la situacion persona a modificar
            SituacionPersonaEntity? situ=await (from st in _db.SituacionesPersona
                                              where st.Id == model.Id
                                              select st).FirstOrDefaultAsync();
            if(situ == null)
            {
                throw new Exception("Situacion persona no encontrada");
            } else
            {
                //Modificamos los datos de la situacion persona
                situ.CodigoSituacion = model.Tipo.Codigo;
                situ.FechaDesde = model.FechaInicio ?? DateTime.Now;
                situ.FechaHasta = model.FechaFin;
                situ.Observaciones = model.Observaciones;
                _db.SituacionesPersona.Update(situ);
                //Guardamos los cambios en la base de datos
                await _db.SaveAsync();
                //Devolvemos el modelo modificado
                return model;
            }
        }
    }
}
