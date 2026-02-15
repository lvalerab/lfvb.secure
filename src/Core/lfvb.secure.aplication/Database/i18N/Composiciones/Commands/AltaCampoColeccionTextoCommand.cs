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
    public class AltaCampoColeccionTextoCommand: IAltaCampoColeccionTextoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _altaElementoCommand;

        public AltaCampoColeccionTextoCommand(IDataBaseService db, IAltaElementoCommand altaElementoCommand)
        {
            _db = db;
            _altaElementoCommand = altaElementoCommand;
        }

        public async Task<CampoColeccionTextoModel> execute(CampoColeccionTextoModel model)
        {
            if(model == null || model.Id!=null || model.Id!=Guid.Empty || model.Coleccion==null || model.Coleccion.Id==null || model.Coleccion.Id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(model));
            } else
            {
                model.Id = await this._altaElementoCommand.execute("cmtx",false);
                await this._db.CamposTextos.AddAsync(new domain.Entities.i18N.CampoTextoEntity
                {
                    Id = model.Id.Value,
                    Nombre = model.Nombre,
                    IdColeccion = model.Coleccion.Id??Guid.Empty
                });
                return model;
            }
        }
    }
}
