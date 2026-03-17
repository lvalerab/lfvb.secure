using AutoMapper;
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
    public class ModificarTextoCommand: IModificarTextoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public ModificarTextoCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<TextoModel> execute(TextoModel texto)
        {
            if(texto.Id == null)
            {
                throw new Exception("El id del texto no puede ser nulo");
            };
            if(texto.Textos == null || texto.Textos.Count == 0)
            {
                throw new Exception("El texto debe tener al menos un idioma");
            };
            
            //Del idioma, no modificamos nada, solo los textos en base si el idioma es compuesto o no
            foreach(var textoIdioma in texto.Textos)
            {
                if(textoIdioma.Id == null)
                {
                   textoIdioma.Id = texto.Id;
                };
                if(textoIdioma.Texto == null)
                {
                    throw new Exception("El texto del texto idioma no puede ser nulo");
                };
                if(textoIdioma.Idioma == null)
                {
                    throw new Exception("El idioma del texto idioma no puede ser nulo");
                };
                IdiomaEntity idioma = await (from idio in _db.Idiomas.Include(i=>i.Agrupaciones)
                                             where idio.Codigo == textoIdioma.Idioma.Codigo
                                             select idio).FirstOrDefaultAsync();
                if (idioma == null)
                {
                    throw new Exception("El idioma del texto idioma no existe");
                }
                else
                {
                    if (idioma.Agrupaciones == null || idioma.Agrupaciones.Count == 0)
                    {
                        //Si el idioma no es compuesto, solo modificamos el texto
                        TextoIdiomaEntity textoIdiomaEntity = await (from text in _db.TextosIdiomas
                                                                     where text.Id == textoIdioma.Id
                                                                      && text.CodIdioma == idioma.Codigo
                                                                     select text).FirstOrDefaultAsync();
                        if (textoIdiomaEntity == null)
                        {
                            //Lo inserto, es posible que se haya añadido un nuevo idioma a un texto ya existente
                            textoIdiomaEntity = new TextoIdiomaEntity
                            {
                                Id = texto.Id.Value,
                                CodIdioma = idioma.Codigo,
                                Contenido = textoIdioma.Texto
                            };
                            await _db.TextosIdiomas.AddAsync(textoIdiomaEntity);
                        }
                        else
                        {
                            textoIdiomaEntity.Contenido = textoIdioma.Texto;
                            _db.TextosIdiomas.Update(textoIdiomaEntity);
                        }
                    }                    
                }
            }
            
            await _db.SaveAsync();
            return texto;
        }
    }
}
