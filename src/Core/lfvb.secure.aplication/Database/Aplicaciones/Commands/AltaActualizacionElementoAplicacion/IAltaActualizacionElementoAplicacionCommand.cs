using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaActualizacionElementoAplicacion
{
    public interface IAltaActualizacionElementoAplicacionCommand
    {
        public Task<ElementoAplicacionModel> Execute(ElementoAplicacionModel elemento);
    }
}
