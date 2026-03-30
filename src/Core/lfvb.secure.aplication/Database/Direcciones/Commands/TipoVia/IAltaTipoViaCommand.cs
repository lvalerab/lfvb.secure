using lfvb.secure.aplication.Database.Direcciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.TipoVia
{
    public interface IAltaTipoViaCommand
    {
        public Task<TipoViaModel> execute(TipoViaModel model);
    }
}
