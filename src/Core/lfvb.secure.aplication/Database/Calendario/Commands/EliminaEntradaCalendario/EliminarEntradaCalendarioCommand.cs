using lfvb.secure.aplication.Database.Calendario.Commands.DesrelacionaEntradaCalendarioUsuario;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.EliminaEntradaCalendario
{
    public class EliminarEntradaCalendarioCommand:IEliminarEntradaCalendarioCommand
    {
        private readonly IDataBaseService _db;
        private IDesrelacionarEntradaCalendarioUsuarioCommand _desrelacionarEntradaCalendario;

        public EliminarEntradaCalendarioCommand(IDataBaseService db, IDesrelacionarEntradaCalendarioUsuarioCommand desrelacionarEntradaCalendario)
        {
            _db = db;
            _desrelacionarEntradaCalendario = desrelacionarEntradaCalendario;
        }

        public async Task<bool> execute(Guid idEntradaCalendario)
        {
            //Lo primero eliminamos las relaciones 
            List<Guid> idCalendarios = await (from ecu in _db.CalendariosUsuariosEntradas
                                              where ecu.IdEntradaCalendario == idEntradaCalendario
                                              select ecu.IdCalendarioUsuario).ToListAsync();
            bool exito=await _desrelacionarEntradaCalendario.execute(new List<Guid> { idEntradaCalendario}, idCalendarios,false);
            if(exito)
            {
                EntradaCalendarioEntity? entidad = await _db.EntradasCalendario.Where(e => e.Id == idEntradaCalendario).FirstOrDefaultAsync();
                if(entidad!=null) {
                    _db.EntradasCalendario.Remove(entidad);
                    await _db.SaveAsync();
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
    }
}
