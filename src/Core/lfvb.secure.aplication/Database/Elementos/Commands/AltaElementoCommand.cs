using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Elementos.Commands
{
    public class AltaElementoCommand : IAltaElementoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public AltaElementoCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }


        public async Task<Guid> execute(string codigoTipoElemento, bool commit = false)
        {
            Guid id = Guid.NewGuid();

            ElementoEntity elemento = new ElementoEntity
            {
                Id = id,
                CodigoTipoElemento = codigoTipoElemento
            };

            await _db.Elementos.AddAsync(elemento);

            if (commit)
            {
                await _db.SaveAsync();
            }
            
            return id;
        }
    }
}
