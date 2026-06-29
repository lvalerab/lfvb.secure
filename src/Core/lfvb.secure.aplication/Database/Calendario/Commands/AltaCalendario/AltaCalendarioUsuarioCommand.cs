using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.AltaCalendario
{
    public class AltaCalendarioUsuarioCommand : IAltaCalendarioUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _altaElementoCommand;

        public AltaCalendarioUsuarioCommand(IDataBaseService db, IAltaElementoCommand altaElementoCommand)
        {
            _db = db;
            _altaElementoCommand = altaElementoCommand;
        }

        public async Task<CalendarioModel> execute(CalendarioModel calendarioModel)
        {
            Guid id=await _altaElementoCommand.execute("caus",false);

            CalendarioUsuarioEntity entity = new CalendarioUsuarioEntity
            {
                Id = id,
                IdUsuario = calendarioModel.Usuario.Id.Value,
                Nombre = calendarioModel.Nombre,
                Entradas = new List<CalendarioUsuarioEntradasEntity>()
            };

            await _db.CalendariosUsuarios.AddAsync(entity);
            await _db.SaveAsync();   

            calendarioModel.Id = id;
            return calendarioModel;
        }
    }
}
