using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class ModificaOpcionCampoColeccionTextoCommand: IModificaOpcionCampoColeccionTextoCommand
    {
        private readonly IDataBaseService _db;

        public ModificaOpcionCampoColeccionTextoCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<OpcionCampoColeccionTextoModel> execute(OpcionCampoColeccionTextoModel model)
        {
            if (model == null || model.Id == null || model.Id == Guid.Empty || model.Campo == null || model.Campo.Id == null || model.Campo.Id == Guid.Empty || model.Texto == null || model.Texto.Id == null || model.Texto.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var entity = await _db.OpcionesTextos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.IdTexto = model.Texto.Id.Value;
                    entity.Opcion = model.Nombre;   
                    _db.OpcionesTextos.Update(entity);
                    await _db.SaveAsync();
                    return model;
                }
                else
                {
                    throw new Exception("No se ha encontrado la opción del campo de la colección de texto");
                }
            }
        }
    }
}
