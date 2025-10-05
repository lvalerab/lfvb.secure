using lfvb.secure.aplication.Database.Aplicaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaPermisoElementoAplicacion
{
    public interface IAltaPermisoElementoAplicacionCommand
    {
        public Task<AltaPermisoElementoAplicacionModel> Execute(AltaPermisoElementoAplicacionModel permiso);
    }
}
