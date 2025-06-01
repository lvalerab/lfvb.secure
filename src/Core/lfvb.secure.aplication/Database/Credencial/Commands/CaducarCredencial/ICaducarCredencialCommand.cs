using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Commands.CaducarCredencial
{
    public interface ICaducarCredencialCommand
    {
        public Task<Int32> execute(Guid idUsuario, String codigoTipoCredencial);
    }
}
