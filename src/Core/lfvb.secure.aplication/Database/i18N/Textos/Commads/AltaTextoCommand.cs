using AutoMapper;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Database.i18N.Textos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.i18N;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Commads
{
    public class AltaTextoCommand: IAltaTextoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private IAltaElementoCommand _cmdAltaElemento;

        public AltaTextoCommand(IDataBaseService db, IMapper mp, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _mp = mp;
            _cmdAltaElemento = cmdAltaElemento; 
        }

        public async Task<TextoModel> execute(TextoModel texto)
        {
            if(texto.Id!=null && texto.Id!=Guid.Empty)
            {
                throw new Exception("El id del texto debe ser nulo");
            }
            if(texto.Textos==null || texto.Textos.Count==0)
            {
                throw new Exception("El texto debe tener al menos un idioma");
            }
            //Creamos el id del texto 
            Guid id = await this._cmdAltaElemento.execute("text");

            //Guardamos el texto en la base de datos

            TextoEntity textoEntity = new TextoEntity
            {
                Id = id
            };

            await _db.Textos.AddAsync(textoEntity);

            texto.Id = id;

            //Guardamos los idiomas del texto
            foreach (var textoIdioma in texto.Textos)
            {
                textoIdioma.Id = id;
                IdiomaEntity idioma = await (from idio in _db.Idiomas.Include(i => i.Agrupaciones)
                                             where idio.Codigo == textoIdioma.Idioma.Codigo
                                             select idio).FirstOrDefaultAsync();
                if (idioma != null)
                {
                    if (idioma.Agrupaciones == null || idioma.Agrupaciones.Count == 0)
                    {
                        //Es un idioma simple
                        TextoIdiomaEntity textoIdiomaEntity = new TextoIdiomaEntity
                        {
                            Id = id,
                            CodIdioma = idioma.Codigo,
                            Contenido = textoIdioma.Texto
                        };
                        await _db.TextosIdiomas.AddAsync(textoIdiomaEntity);
                    }
                    else
                    {
                        if (texto.Columnas == null || texto.Columnas.Columna1 ==null || texto.Columnas.Columna2 == null  || texto.Columnas.Columna1.Idioma== null  ||texto.Columnas.Columna2.Idioma == null)
                        {
                            throw new Exception($"El idioma {textoIdioma.Idioma.Codigo} es un idioma agrupado, por lo que debe tener columnas");
                        }
                        else
                        {
                            ColumnaTextoIdiomaEntity columnaTextoIdiomaEntity = new ColumnaTextoIdiomaEntity
                            {
                                Id = id,
                                CodIdioma = texto.Columnas.Columna1.Idioma.Codigo,
                                Contenido = texto.Columnas.Columna1.Texto
                            };
                            await _db.ColumnasTextosIdiomas.AddAsync(columnaTextoIdiomaEntity);
                            ColumnaTextoIdiomaEntity columnaTextoIdiomaEntity2 = new ColumnaTextoIdiomaEntity
                            {
                                Id = id,
                                CodIdioma = texto.Columnas.Columna2.Idioma.Codigo,
                                Contenido = texto.Columnas.Columna2.Texto
                            };
                            await _db.ColumnasTextosIdiomas.AddAsync(columnaTextoIdiomaEntity);
                        }
                    }
                }
                else
                {
                    throw new Exception($"El idioma {textoIdioma.Idioma.Codigo} no existe");
                }                
            }
            await _db.SaveAsync();
            return texto;
        }
    }
}
