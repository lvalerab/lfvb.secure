using lfvb.secure.aplication.Database.i18N.Textos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Queries
{
    public interface IBuscadorTextosQuery
    {
        public Task<List<Guid>> execute(string busqueda, bool OmitirMayusculas=false, List<string>? idiomas=null);
        public Task<List<TextoModel>> executeModel(string busqueda, bool OmitirMayusculas = false, List<string>? idiomas = null);
    }
}
