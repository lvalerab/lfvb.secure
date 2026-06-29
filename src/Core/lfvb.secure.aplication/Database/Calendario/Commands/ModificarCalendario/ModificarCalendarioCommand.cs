using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.ModificarCalendario
{
    public class ModificarCalendarioCommand:IModificarCalendarioCommand
    {
        private readonly IDataBaseService _db;

        public ModificarCalendarioCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<CalendarioModel> execute(CalendarioModel calendarioModel)
        {
            var entity = await _db.CalendariosUsuarios.FindAsync(calendarioModel.Id);
            if (entity == null)
            {
                throw new Exception("Calendario no encontrado");
            }
            entity.Nombre = calendarioModel.Nombre;
            _db.CalendariosUsuarios.Update(entity);
            await _db.SaveAsync();
            return calendarioModel;
        }
    }
}
