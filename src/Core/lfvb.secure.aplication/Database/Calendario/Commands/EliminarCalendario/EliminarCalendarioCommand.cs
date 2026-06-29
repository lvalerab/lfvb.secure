using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.EliminarCalendario
{
    public class EliminarCalendarioCommand:IEliminarCalendarioCommand
    {
        private readonly IDataBaseService _db;
        
        public EliminarCalendarioCommand(IDataBaseService db)
        {
            _db = db;
        }


        public async Task<bool> execute(Guid idCalendario, Guid? idCalendarioDestino = null)
        {
            var entity = await _db.CalendariosUsuarios.FindAsync(idCalendario);
            if (entity == null)
            {
                throw new Exception("Calendario no encontrado");
            }
            if (idCalendarioDestino.HasValue)
            {
                var entityDestino = await _db.CalendariosUsuarios.FindAsync(idCalendarioDestino.Value);
                if (entityDestino == null)
                {
                    throw new Exception("Calendario destino no encontrado");
                }
                // Mover entradas al calendario destino
                foreach (var entrada in entity.Entradas)
                {
                    entrada.IdCalendarioUsuario = idCalendarioDestino.Value;
                    entityDestino.Entradas.Add(entrada);
                }
            }
            _db.CalendariosUsuarios.Remove(entity);
            await _db.SaveAsync();
            return true;
        }
    }
}
