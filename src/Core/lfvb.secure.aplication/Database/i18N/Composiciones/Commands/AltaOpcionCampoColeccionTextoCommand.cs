using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class AltaOpcionCampoColeccionTextoCommand: IAltaOpcionCampoColeccionTextoCommand
    {
        private readonly IDataBaseService _db;  
        private readonly IAltaElementoCommand _altaElementoCommand;

        public AltaOpcionCampoColeccionTextoCommand(IDataBaseService db, IAltaElementoCommand altaElementoCommand)
        {
            _db = db;
            _altaElementoCommand = altaElementoCommand;
        }

        public async Task<OpcionCampoColeccionTextoModel> execute(OpcionCampoColeccionTextoModel model)
        {
            if (model == null || model.Id != null || model.Id != Guid.Empty || model.Campo == null || model.Campo.Id == null || model.Campo.Id == Guid.Empty || model.Texto==null || model.Texto.Id==null || model.Texto.Id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                model.Id = await _altaElementoCommand.execute("optx", false);
                await _db.OpcionesTextos.AddAsync(new domain.Entities.i18N.OpcionTextoEntity
                {
                    Id = model.Id.Value,
                    IdCampo = model.Campo.Id.Value,
                    IdTexto = model.Texto.Id.Value,
                });
                return model;
            }
        }
    }
}
