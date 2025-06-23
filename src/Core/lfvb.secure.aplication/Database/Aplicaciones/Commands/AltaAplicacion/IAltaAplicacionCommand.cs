using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaAplicacion
{
    public interface IAltaAplicacionCommand
    {
        public Task<AplicacionModel> Execute(AltaAplicacionModel model);
    }
}
