using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.ModificaEntradaCalendario
{
    public class ModificarEntradaCalendarioCommand: IModificarEntradaCalendarioCommand
    {
        private readonly IDataBaseService _db;

        public ModificarEntradaCalendarioCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<EntradaCalendarioModel> execute(EntradaCalendarioModel model)
        {
            EntradaCalendarioEntity? entidad = await _db.EntradasCalendario.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (entidad == null)
            {
                throw new Exception("No se ha encontrado la entrada indicada");
            } else
            {
                entidad.Titulo = model.Titulo;
                entidad.Descripcion = model.Descripcion;
                entidad.FechaInicio= model.FechaInicio;
                entidad.FechaFin = model.FechaFin??DateTime.MaxValue;
                _db.EntradasCalendario.Update(entidad);
                await _db.SaveAsync();
            }
            return model;
        }
    }
}
