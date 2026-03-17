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
    public class GetTextoQuery: IGetTextoQuery
    {
        private readonly IDataBaseService _db;

        public GetTextoQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<TextoModel?> execute(Guid id)
        {
            TextoModel? result=await (from tx in _db.Textos                                            
                                            .Include(tx => tx.TextosIdiomas)
                                      where tx.Id == id
                                     select new TextoModel
                                     {
                                         Id = tx.Id,
                                         Variables = (from v in _db.VariablesTextos
                                                      where v.Id == tx.Id
                                                      select new VariableTextoModel
                                                      {
                                                          Id = v.Id,
                                                          Variable = v.Variable
                                                      }).ToList(),
                                         Textos = (from ti in _db.TextosIdiomas
                                                                .Include(ti => ti.Idioma)   
                                                   where ti.Id == tx.Id
                                                   select new TextoIdiomaModel
                                                   {
                                                       Id = ti.Id,
                                                       Idioma= new IdiomaModel
                                                       {
                                                           Nombre = ti.Idioma.Nombre,
                                                           Codigo = ti.Idioma.Codigo
                                                       },   
                                                       Texto=ti.Contenido
                                                   }).ToList()
                                     }).FirstOrDefaultAsync();


            if(result != null)
            {

                TextoIdiomaModel columna1 = await (from tc in _db.ColumnasTextosIdiomas.Include(tc => tc.AgrupacionIdioma)
                                                   join idio in _db.Idiomas on tc.CodIdiomaRelacionado equals idio.Codigo
                                                   where tc.Id == result.Id && tc.AgrupacionIdioma.Orden == 0
                                                   select new TextoIdiomaModel
                                                   {
                                                       Id = tc.Id,
                                                       Idioma = new IdiomaModel
                                                       {
                                                           Nombre = idio.Nombre,
                                                           Codigo = idio.Codigo
                                                       },
                                                       Texto = tc.Contenido
                                                   }).FirstOrDefaultAsync();

                TextoIdiomaModel columna2 = await (from tc in _db.ColumnasTextosIdiomas.Include(tc => tc.AgrupacionIdioma)
                                                   join idio in _db.Idiomas on tc.CodIdiomaRelacionado equals idio.Codigo
                                                   where tc.Id == result.Id && tc.AgrupacionIdioma.Orden == 0
                                                   select new TextoIdiomaModel
                                                   {
                                                       Id = tc.Id,
                                                       Idioma = new IdiomaModel
                                                       {
                                                           Nombre = idio.Nombre,
                                                           Codigo = idio.Codigo
                                                       },
                                                       Texto = tc.Contenido
                                                   }).FirstOrDefaultAsync();
                if (columna1 != null || columna2 != null)
                {
                    result.Columnas = new TextoColumnaIdiomaModel
                    {
                        Id = result.Id,
                        Columna1 = columna1,
                        Columna2 = columna2
                    };
                }
            }   


            return result;
        }
    }
}
