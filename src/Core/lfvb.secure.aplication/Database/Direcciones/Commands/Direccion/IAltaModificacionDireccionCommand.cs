using lfvb.secure.aplication.Database.Direcciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Direccion
{
    public interface IAltaModificacionDireccionCommand
    {
        public Task<DireccionModel> execute(DireccionModel model);
    }
}
