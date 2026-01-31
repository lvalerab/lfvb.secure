using AutoMapper;
using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.i18N;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Commands
{
    public class ModificarIdiomaCommand:IModificarIdiomaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public ModificarIdiomaCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<IdiomaModel> execute(IdiomaModel idioma)
        {
            if (idioma.Codigo != "")
            {
                throw new Exception("Debe indicarse un código del idioma");
            }

            if (idioma.Nombre != "")
            {
                throw new Exception("Debe indicarse un nombre del idioma");
            }

            IdiomaEntity entidad = await (from i in _db.Idiomas
                                          where i.Codigo == idioma.Codigo
                                          select i).FirstOrDefaultAsync();

            if(entidad==null)
            {
                throw new Exception("No se ha encontrado el idioma");
            }

            entidad.Nombre = idioma.Nombre;

            _db.Idiomas.Update(entidad);

            await _db.SaveAsync();

            return idioma;
        }
    }
}
