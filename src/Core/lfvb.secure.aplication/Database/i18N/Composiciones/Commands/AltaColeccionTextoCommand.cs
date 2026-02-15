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
    public class AltaColeccionTextoCommand: IAltaColeccionTextoCommand
    {
        private readonly IDataBaseService _db;
        private IAltaElementoCommand _altaElementoCommand;

        public AltaColeccionTextoCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _altaElementoCommand = cmdAltaElemento;
        }

        public async Task<ColeccionTextoModel> execute(ColeccionTextoModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if(model.Id == null || model.Id == Guid.Empty)
            {
                Guid id = await this._altaElementoCommand.execute("cltx", false);
                model.Id = id;
                await _db.ColeccionesTextos.AddAsync(new domain.Entities.i18N.ColeccionTextoEntity
                {
                    Id = id,
                    Nombre = model.Nombre,
                    Descripcion = model.Detalle
                });

                await _db.SaveAsync();
                return model;
            } else
            {
                throw new Exception("El Id tiene que ser nulo");
            }
        }
    }
}
