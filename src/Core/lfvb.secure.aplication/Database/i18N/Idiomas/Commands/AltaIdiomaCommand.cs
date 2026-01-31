using AutoMapper;
using lfvb.secure.aplication.Database.Elementos.Commands;
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
    public class AltaIdiomaCommand:IAltaIdiomaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private IAltaElementoCommand _cmdAltaElem;

        public AltaIdiomaCommand(IDataBaseService db, IMapper mp, IAltaElementoCommand cmdAltaElem)
        {
            _db = db;
            _mp = mp;
            _cmdAltaElem = cmdAltaElem;
        }

        public async Task<IdiomaModel> execute(IdiomaModel idioma)
        {
            
            //Comprobamos los datos
            if(idioma.Codigo!="")
            {
                throw new Exception("Debe indicarse un código del idioma");
            }

            if(idioma.Nombre!="")
            {
                throw new Exception("Debe indicarse un nombre del idioma");
            }

            if(idioma.Multiple && idioma.Componentes.Count()<=0)
            {
                throw new Exception("Se ha indicado que es un idioma compuesto, pero no se han definido los componentes de dicho idioma");
            }

            bool existe = (await _db.Idiomas.Where(i => i.Codigo == idioma.Codigo).ToListAsync()).Count > 0;
            if(existe)
            {
                throw new Exception("Este codigo de idioma ya esta dado de alta, debe indicar un nuevo codigo");
            }

            //Creamos el id
            Guid? id =await  _cmdAltaElem.execute("idio", false);

            IdiomaEntity entidad = new IdiomaEntity
            {
                Codigo = idioma.Codigo,
                Id = id ?? Guid.Empty,
                Nombre = idioma.Nombre
            };

            _db.Idiomas.Add(entidad);

            if(idioma.Multiple)
            {
                foreach(IdiomaModel idio in idioma.Componentes)
                {
                    AgrupacionIdiomaEntity agio = new AgrupacionIdiomaEntity
                    {
                        Codigo = idioma.Codigo,
                        CodigoIdiomaRelacionado = idio.Codigo,
                        Orden = idio.Orden??0
                    };
                    _db.AgrupacionesIdiomas.Add(agio);
                }
            }

            await _db.SaveAsync();

            return idioma;
        }
    }
}
