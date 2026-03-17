using AutoMapper;
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
    public class GetAllTextos: IGetAllTextos
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;    

        public GetAllTextos(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TextoModel>> execute()
        {
            List<TextoModel> result = await (from tx in _db.Textos
                                                                .Include(tx => tx.Variables)
                                             select new TextoModel
                                             {
                                                 Id= tx.Id,
                                                 Textos = (from ti in _db.TextosIdiomas.Include(ti=>ti.Idioma)
                                                                                               where ti.Id == tx.Id
                                                                                               select new TextoIdiomaModel
                                                                                               {
                                                                                                   Id = ti.Id,
                                                                                                   Idioma = new IdiomaModel
                                                                                                   {   
                                                                                                       Nombre = ti.Idioma.Nombre,
                                                                                                       Codigo = ti.Idioma.Codigo
                                                                                                   },
                                                                                                   Texto = ti.Contenido
                                                                                               }).ToList(),
                                                Variables = (from tv in _db.VariablesTextos
                                                                where tv.IdTexto == tx.Id  
                                                                select new VariableTextoModel
                                                                {
                                                                    Id = tv.Id,
                                                                    Texto=new TextoModel
                                                                    {
                                                                        Id = tv.IdTexto
                                                                    },
                                                                    Variable = tv.Variable  
                                                                }).ToList()
                                             }).ToListAsync();

            foreach(var texto in result)
            {
                TextoIdiomaModel columna1 = await (from tc in _db.ColumnasTextosIdiomas.Include(tc => tc.AgrupacionIdioma)
                                                   join id in _db.Idiomas on tc.CodIdiomaRelacionado equals id.Codigo
                                                   where tc.Id == texto.Id && tc.AgrupacionIdioma.Orden == 0
                                                   select new TextoIdiomaModel
                                                   {
                                                       Id = tc.Id,
                                                       Idioma = new IdiomaModel
                                                       {
                                                           Nombre = id.Nombre,
                                                           Codigo = id.Codigo
                                                       },
                                                       Texto = tc.Contenido
                                                   }).FirstOrDefaultAsync();

                TextoIdiomaModel columna2 = await (from tc in _db.ColumnasTextosIdiomas.Include(tc => tc.AgrupacionIdioma)
                                                   join id in _db.Idiomas on tc.CodIdiomaRelacionado equals id.Codigo
                                                   where tc.Id == texto.Id && tc.AgrupacionIdioma.Orden == 0
                                                   select new TextoIdiomaModel
                                                   {
                                                       Id = tc.Id,
                                                       Idioma = new IdiomaModel
                                                       {
                                                           Nombre = id.Nombre,
                                                           Codigo = id.Codigo
                                                       },
                                                       Texto = tc.Contenido
                                                   }).FirstOrDefaultAsync();
                if(columna1 != null || columna2!=null)
                {
                    texto.Columnas=new TextoColumnaIdiomaModel
                    {
                        Id = texto.Id,
                        Columna1 = columna1,
                        Columna2 = columna2
                    };
                }
            }   

            return result;
        }
    }
}
