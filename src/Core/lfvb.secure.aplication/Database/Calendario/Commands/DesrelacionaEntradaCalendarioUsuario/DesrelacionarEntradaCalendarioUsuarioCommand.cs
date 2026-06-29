using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.DesrelacionaEntradaCalendarioUsuario
{
    internal class DesrelacionarEntradaCalendarioUsuarioCommand: IDesrelacionarEntradaCalendarioUsuarioCommand
    {
        private readonly IDataBaseService _db;
        
        public DesrelacionarEntradaCalendarioUsuarioCommand(IDataBaseService db)
        {
            _db = db;
        }   

        public async Task<bool> execute(Guid idEntrada, Guid idCalendario, bool commit = false)
        {
            CalendarioUsuarioEntradasEntity? entidad=await _db.CalendariosUsuariosEntradas.Where(x=>x.IdEntradaCalendario==idEntrada && x.IdCalendarioUsuario==idCalendario).FirstOrDefaultAsync();
            if(entidad != null)
            {
                _db.CalendariosUsuariosEntradas.Remove(entidad);
                if(commit)
                {
                    await _db.SaveAsync();
                }
                return true;
            }
            return false;
        }

        public async Task<bool> execute(List<Guid> idEntradas, Guid idCalendario, bool commit = false)
        {
            bool result = true;
            foreach (var idEntrada in idEntradas)
            {
                result = result && await execute(idEntrada, idCalendario, false);
            }
            if (result && commit)
            {
                await _db.SaveAsync();
            }
            return result;
        }

        public async Task<bool> execute(List<Guid> idEntradas, List<Guid> idCalendarios, bool commit = false)
        {
            bool result = true;
            foreach(Guid idCalendario in idCalendarios)
            {
                foreach(Guid idEntrada in idEntradas)
                {
                    result=result && await execute(idEntrada,idCalendario,false);
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
