using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.RelacionaEntradaCalendarioUsuario
{
    public interface IRelacionarEntradaCalendarioUsuarioCommand
    {
        public Task<bool> execute(Guid idEntrada, Guid idCalendario, bool commit=false);
        public Task<bool> execute(List<Guid> idEntradas, Guid idCalendario, bool commit=false);
        public Task<bool> execute(List<Guid> idEntradas, List<Guid> idCalendarios, bool commit=false);
    }
}
