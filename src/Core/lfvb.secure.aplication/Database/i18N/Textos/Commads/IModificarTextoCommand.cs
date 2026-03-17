using lfvb.secure.aplication.Database.i18N.Textos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Commads
{
    public interface IModificarTextoCommand
    {
        public Task<TextoModel> execute(TextoModel texto);  
    }
}
