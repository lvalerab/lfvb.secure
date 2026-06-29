using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.RelacionaEntradaCalendarioUsuario
{
    public class RelacionarEntradaCalendarioUsuarioCommand: IRelacionarEntradaCalendarioUsuarioCommand
    {
        private readonly IDataBaseService _db;

        public RelacionarEntradaCalendarioUsuarioCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid idEntrada, Guid idCalendario, bool commit = false)
        {
            var aux = await _db.CalendariosUsuariosEntradas.Where(x => x.IdCalendarioUsuario == idCalendario && x.IdEntradaCalendario == idEntrada).FirstOrDefaultAsync();

            if (aux == null)
            {

                CalendarioUsuarioEntradasEntity entity = new CalendarioUsuarioEntradasEntity
                {
                    IdCalendarioUsuario = idCalendario,
                    IdEntradaCalendario = idEntrada
                };

                await _db.CalendariosUsuariosEntradas.AddAsync(entity);
                if (commit)
                {
                    await _db.SaveAsync();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> execute(List<Guid> idEntradas, Guid idCalendario, bool commit = false)
        {
            bool result = true;
            foreach (var idEntrada in idEntradas)
            {
                result = result && await execute(idEntrada, idCalendario, false);
            }
            if(result && commit)
            {
                await _db.SaveAsync();
            }
            return result;
        }

        public async Task<bool> execute(List<Guid> idEntradas, List<Guid> idCalendarios, bool commit = false)
        {
            bool result = true;
            foreach (var idCalendario in idCalendarios)
            {
                foreach (var idEntrada in idEntradas)
                {
                    result = result && await execute(idEntrada, idCalendario, false);
                }
            }
            if(result && commit)
            {
                await _db.SaveAsync();
            }
            return result;
        }
    }
}
