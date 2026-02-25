using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using lfvb.secure.aplication.Database.i18N.Textos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Queries
{
    public class BuscadorTextosQuery : IBuscadorTextosQuery
    {
        private readonly IDataBaseService _db;
        public BuscadorTextosQuery(IDataBaseService db)
        {
            _db = db;
        }


        public async Task<List<Guid>> execute(string busqueda, bool OmitirMayusculas = false, List<string>? idiomas = null)
        {
            List<Guid> result = await (from ti in _db.TextosIdiomas.Include(ti => ti.Idioma)
                                       where ((!OmitirMayusculas && ti.Contenido.Contains(busqueda)) || (OmitirMayusculas && ti.Contenido.ToLower().Contains(busqueda.ToLower()))) && (idiomas == null || idiomas.Contains(ti.Idioma.Codigo))
                                       select ti.Id).ToListAsync();

            result.AddRange(await (from tc in _db.ColumnasTextosIdiomas.Include(tc => tc.AgrupacionIdioma)
                                   where ((!OmitirMayusculas && tc.Contenido.Contains(busqueda)) || (OmitirMayusculas && tc.Contenido.ToLower().Contains(busqueda.ToLower()))) && (idiomas == null || idiomas.Contains(tc.AgrupacionIdioma.CodigoIdiomaRelacionado))
                                   select tc.Id).ToListAsync());

            return result;
        }

        public async Task<List<TextoModel>> executeModel(string busqueda, bool OmitirMayusculas = false, List<string>? idiomas = null)
        {
            List<Guid> ids = await execute(busqueda, OmitirMayusculas, idiomas);

            List<TextoModel> result = await (from t in _db.Textos.Include(t => t.TextosIdiomas).Include(t => t.Variables)
                                             where ids.Contains(t.Id)
                                             select new TextoModel
                                             {
                                                 Id = t.Id,
                                                 Textos = t.TextosIdiomas.Select(ti => new TextoIdiomaModel
                                                 {
                                                     Id = ti.Id,
                                                     Texto = ti.Contenido,
                                                     Idioma = new IdiomaModel
                                                     {
                                                         Codigo = ti.Idioma.Codigo,
                                                         Nombre = ti.Idioma.Nombre
                                                     }
                                                 }).ToList(),
                                                 Variables = t.Variables.Select(v => new VariableTextoModel
                                                 {
                                                     Id = v.Id,
                                                     Variable = v.Variable
                                                 }).ToList()
                                             }).ToListAsync();
            return result;
        }
    }
}
