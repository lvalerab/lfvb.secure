using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Personas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Commands.RelacionarElementoPersona
{
    public class RelacionarElementoPersonaCommand: IRelacionarElementoPersonaCommand
    {
        private readonly IDataBaseService _db;

        public RelacionarElementoPersonaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid idPersona, Guid idElemento, bool commit = true)
        {
            bool result = false;
            var existe = await (from re in _db.ElementosPersona
                                where re.IdPersona == idPersona && re.IdElemento == idElemento
                                select re).FirstOrDefaultAsync();

            if (existe == null)
            {
                var relacion = new ElementoPersonaEntity
                {
                    IdPersona = idPersona,
                    IdElemento = idElemento
                };
                _db.ElementosPersona.Add(relacion);
                if (commit)
                    await _db.SaveAsync();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
