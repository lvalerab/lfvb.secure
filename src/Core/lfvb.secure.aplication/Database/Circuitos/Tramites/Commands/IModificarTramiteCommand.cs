using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Tramites.Commands
{
    public interface IModificarTramiteCommand
    {
        Task<TramiteModel> execute(TramiteModel modelo);
    }
}
