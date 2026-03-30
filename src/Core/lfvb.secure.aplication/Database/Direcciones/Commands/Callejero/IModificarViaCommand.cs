using lfvb.secure.aplication.Database.Direcciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Callejero
{
    public interface IModificarViaCommand
    {
        Task<CallejeroModel> execute(CallejeroModel model, bool transacion = true); 
    }
}
