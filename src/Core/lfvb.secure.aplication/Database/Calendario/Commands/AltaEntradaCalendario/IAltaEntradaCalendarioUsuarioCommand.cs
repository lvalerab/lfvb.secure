using lfvb.secure.aplication.Database.Calendario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.AltaEntradaCalendario
{
    public interface IAltaEntradaCalendarioUsuarioCommand
    {
        public Task<EntradaCalendarioModel> execute(EntradaCalendarioModel entrada,Guid idCreador, List<Guid> idUsuarios, string TipoCalendario);
        public Task<EntradaCalendarioModel> execute(EntradaCalendarioModel entrada,Guid idCreador, Guid idUsuario, Guid idCalendario);
    }
}
